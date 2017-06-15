using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Catfood.Shapefile;
using DotSpatial.Topology;
using DotSpatial.Data;

namespace Conversor
{
    public partial class Form1 : Form
    {
        int cont = 0;
        NpgsqlConnection conexao = new NpgsqlConnection(
            "Server=127.0.0.1;Port=5432; User Id=postgres; Password=postgres;" +
            "Database=db_aula;");

        IList<Ponto> positions = new List<Ponto>();
        IList<string> Srids = new List<string>();
        public Form1()
        {
            InitializeComponent();

            cbGeometria.SelectedIndexChanged += CbGeometria_SelectedIndexChanged;
            preencheSrids();
        }

        private void preencheSrids()
        {

            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;

                cmd.CommandText = "SELECT  SRID||' '||SUBSTR(SUBSTR(SRTEXT, POSITION('[\"' IN SRTEXT)+2),1, POSITION('\",'" +
                    " IN SUBSTR(SRTEXT, POSITION('[\"' IN SRTEXT)+2))-1) FROM SPATIAL_REF_SYS";


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Srids.Add(reader.GetString(0));
                    }
                }
            }
            conexao.Close();
            cbSrid.DataSource = Srids;
        }


        private void CbGeometria_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbNome.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = null;
            positions.Clear();
            if ((!tbNome.Text.Equals(string.Empty)) && (cbGeometria.SelectedIndex >= 0))
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //cria a tabela para a geometria
                    criaTabela(tbNome.Text);

                    //tipo de geometria selecionado
                    string[] dados = cbSrid.SelectedItem.ToString().Split(' ');
                    string tipogeo = dados[0];

                    //selecionado o tipo txt
                    if (rbTxt.Checked == true)
                    {
                        ProcessarArquivo(openFileDialog1.FileName);
                        dgvData.DataSource = positions;
                        GerarArquivoShp();
                    }

                    //selecionado o tipo Shp
                    if (rbShp.Checked == true)
                    {
                        ProcessarArquivoShp(openFileDialog1.FileName);
                        dgvData.DataSource = positions;
                    }

                    //selecionado o tipo xls
                    if (rbXls.Checked == true)
                    {
                        ProcessarArquivoXls(openFileDialog1.FileName);
                        dgvData.DataSource = positions;
                        GerarArquivoShp();
                    }
                    
                    //Tipo ponto selecionado
                    if (cbGeometria.SelectedIndex == 0)
                    {
                        insereTabelaTipo("tb_" + tbNome.Text, 0);
                        for (int i = 0; i < positions.Count; i++)
                        {
                            inserePonto(tbNome.Text, positions[i].PosX, positions[i].PosY, tipogeo);
                        }
                    }

                    //Tipo linha selecionado
                    if (cbGeometria.SelectedIndex == 1)
                    {
                        Array x = new float[positions.Count];
                        Array y = new float[positions.Count];

                        //Arrumar a passager do vetor
                        insereTabelaTipo("tb_" + tbNome.Text, 1);
                        for (int i = 0; i < positions.Count; i++)
                        {
                            x.SetValue(positions[i].PosX, i);
                            y.SetValue(positions[i].PosY, i);
                        }

                        insereLinha(tbNome.Text, x, y, tipogeo);
                    }

                    //Tipo Poligono selecionado
                    if (cbGeometria.SelectedIndex == 2)
                    {
                        Array x = new float[positions.Count + 1];
                        Array y = new float[positions.Count + 1];

                        //Arrumar a passager do vetor
                        insereTabelaTipo("tb_" + tbNome.Text, 2);
                        for (int i = 0; i < positions.Count; i++)
                        {
                            x.SetValue(positions[i].PosX, i);
                            y.SetValue(positions[i].PosY, i);
                        }

                        //garantindo que a ultima posicao seja igual a primeira
                        x.SetValue(x.GetValue(0), positions.Count);
                        y.SetValue(y.GetValue(0), positions.Count);

                        inserePoligono(tbNome.Text, x, y, tipogeo);
                    }
                }

            }
            else
            {
                MessageBox.Show("Preencha o Nome/Tipo de geometria");
            }

        }

        private void OpenShapefile(string path)
        {
            // clear any shapefiles the map is currently displaying
            this.sfMap1.ClearShapeFiles();

            // open the shapefile passing in the path, display name of the shapefile and
            // the field name to be used when rendering the shapes (we use an empty string
            // as the field name (3rd parameter) can not be null)
            this.sfMap1.AddShapeFile(path, "ShapeFile", "");

            // read the shapefile dbf field names and set the shapefiles's RenderSettings
            // to use the first field to label the shapes.
            EGIS.ShapeFileLib.ShapeFile sf = this.sfMap1[0];
            sf.RenderSettings.FieldName = sf.RenderSettings.DbfReader.GetFieldNames()[0];
            sf.RenderSettings.UseToolTip = true;
            sf.RenderSettings.ToolTipFieldName = sf.RenderSettings.FieldName;
            sf.RenderSettings.IsSelectable = true;

            //select the first record
            sf.SelectRecord(0, true);

        }

        private void ProcessarArquivoShp(string fileName)
        {
            OpenShapefile(fileName);

            using (Catfood.Shapefile.Shapefile shapefile = new Catfood.Shapefile.Shapefile(fileName))
            {
                foreach (Catfood.Shapefile.Shape shape in shapefile)
                {
                    switch (shape.Type)
                    {
                        case Catfood.Shapefile.ShapeType.Point:
                            ShapePoint shapePoint = shape as ShapePoint;
                            var ponto = new Ponto()
                            {
                                PosX = (float)shapePoint.Point.X,
                                PosY = (float)shapePoint.Point.Y
                            };
                            positions.Add(ponto);
                            break;

                        case Catfood.Shapefile.ShapeType.Polygon:

                            ShapePolygon shapePolygon = shape as ShapePolygon;
                            for (int i = 0; i < shapePolygon.Parts.Count; i++)
                            {
                                for (int j = 0; j < shapePolygon.Parts[i].Length; j++)
                                {
                                    var ponto2 = new Ponto()
                                    {
                                        PosX = (float)shapePolygon.Parts[i][j].X,
                                        PosY = (float)shapePolygon.Parts[i][j].Y
                                    };
                                    positions.Add(ponto2);
                                }
                            }
                            break;

                        case Catfood.Shapefile.ShapeType.PolyLine:
                            ShapePolyLine shapePolyline = shape as ShapePolyLine;
                            for (int i = 0; i < shapePolyline.Parts.Count; i++)
                            {
                                for (int j = 0; j < shapePolyline.Parts[i].Length; j++)
                                {
                                    var ponto2 = new Ponto()
                                    {
                                        PosX = (float)shapePolyline.Parts[i][j].X,
                                        PosY = (float)shapePolyline.Parts[i][j].Y
                                    };
                                    positions.Add(ponto2);
                                }
                            }
                            break;
                    }
                }

            }
        }


        private void ProcessarArquivoXls(string fileName)
        {
            using (SpreadsheetDocument myDoc = SpreadsheetDocument.Open(fileName, true))
            {
                WorkbookPart workbookPart = myDoc.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData =
                worksheetPart.Worksheet.Elements<SheetData>().First();

                foreach (var row in sheetData.Elements<Row>().ToArray())
                {
                    var position = new Ponto
                    {
                        PosX = (float)Convert.ToDouble(row.Elements<Cell>().ToList()[0].CellValue.Text),
                        PosY = (float)Convert.ToDouble(row.Elements<Cell>().ToList()[1].CellValue.Text)
                    };
                    positions.Add(position);
                }
            }
        }

        private void inserePoligono(string text, Array x, Array y, string tipogeo)
        {
            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;
                cmd.CommandText = "SELECT f_insere_poligono(@text, @x,  @y, @tipogeo)";
                cmd.Parameters.AddWithValue("@text", text);
                cmd.Parameters.AddWithValue("@x", x);
                cmd.Parameters.AddWithValue("@y", y);
                cmd.Parameters.AddWithValue("@tipogeo", tipogeo);

                cmd.ExecuteNonQuery();
            }
            conexao.Close();
        }

        private void insereLinha(string text, Array x, Array y, string tipogeo)
        {
            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;

                cmd.CommandText = "SELECT f_insere_linha(@text, @x,  @y, @tipogeo)";
                cmd.Parameters.AddWithValue("@text", text);
                cmd.Parameters.AddWithValue("@x", x);
                cmd.Parameters.AddWithValue("@y", y);
                cmd.Parameters.AddWithValue("@tipogeo", tipogeo);

                cmd.ExecuteNonQuery();
            }
            conexao.Close();
        }

        private void insereTabelaTipo(string v1, int v2)
        {
            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;
                try
                {
                    cmd.CommandText = "SELECT f_insere_tabela_tipo(@v1, @v2)";
                    cmd.Parameters.AddWithValue("@v1", v1);
                    cmd.Parameters.AddWithValue("@v2", v2);

                    cmd.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    cmd.CommandText = "create table tb_tabelatipo(gid serial, tabelanome text, tipo integer)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT f_insere_tabela_tipo(@v1, @v2)";
                    cmd.Parameters.AddWithValue("@v1", v1);
                    cmd.Parameters.AddWithValue("@v2", v2);

                    cmd.ExecuteNonQuery();
                }
            }
            conexao.Close();
        }

        private void criaTabela(string text)
        {
            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;
                try
                {
                    cmd.CommandText = "SELECT f_create_table(@text)";
                    cmd.Parameters.AddWithValue("@text", text);

                    cmd.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    MessageBox.Show("Ja existe uma tabela com este nome");
                }
            }
            conexao.Close();
        }

        private void inserePonto(string text, float x, float y, string tipogeo)
        {
            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;

                cmd.CommandText = "SELECT f_insere_ponto(@text, @x,@y, @tipogeo)";
                cmd.Parameters.AddWithValue("@text", text);
                cmd.Parameters.AddWithValue("@x", x);
                cmd.Parameters.AddWithValue("@y", y);
                cmd.Parameters.AddWithValue("@tipogeo", tipogeo);
                cmd.ExecuteNonQuery();
            }
            conexao.Close();
        }

        private void ProcessarArquivo(String nomeArquivo)
        {
            string linhaLida;
            var arquivo = new System.IO.StreamReader(@nomeArquivo);
            int countlinha = 0;
            while ((linhaLida = arquivo.ReadLine()) != null)
            {
                string novalinha;
                novalinha = linhaLida.Replace(" ", ";").Replace("\t", ";").Replace(";;", ";");
                var dadosLidos = novalinha.Split(';');

                var position = new Ponto
                {
                    PosX = (float)Convert.ToDouble(dadosLidos[0]),
                    PosY = (float)Convert.ToDouble(dadosLidos[1])
                };
                positions.Add(position);
                countlinha++;
            }
            arquivo.Close();
        }

        private void GerarArquivoShp()
        {
            cont++;
            string fileName = @".\temp"+cont+".shp";
            //ponto
            if (cbGeometria.SelectedIndex == 0)
            {
                Coordinate[] c = new Coordinate[dgvData.Rows.Count];
                Feature f = new Feature();
                FeatureSet fs = new FeatureSet(f.FeatureType);

                for (int j = 0; j < dgvData.Rows.Count; j++)
                {
                    c[j] = new Coordinate(Convert.ToDouble(dgvData.Rows[j].Cells[0].Value.ToString()),
                        Convert.ToDouble(dgvData.Rows[j].Cells[1].Value.ToString()));
                    fs.Features.Add(c[j]);
                }
                fs.SaveAs(fileName, true);
            }

            //linha
            if (cbGeometria.SelectedIndex == 1)
            {
                Feature f = new Feature();
                FeatureSet fs = new FeatureSet(f.FeatureType);
                for (int ii = 0; ii < 1; ii++)
                {
                    Coordinate[] coord = new Coordinate[dgvData.Rows.Count];
                    for (int j = 0; j < dgvData.Rows.Count; j++)
                    {
                        coord[j] = new Coordinate(Convert.ToDouble(dgvData.Rows[j].Cells[0].Value.ToString()),
                            Convert.ToDouble(dgvData.Rows[j].Cells[1].Value.ToString()));
                    }
                    LineString ls = new LineString(coord);
                    f = new Feature(ls);
                    fs.Features.Add(f);
                }
                fs.SaveAs(fileName, true);
            }

            //poligono
            if (cbGeometria.SelectedIndex == 2)
            {
                Polygon[] pg = new Polygon[1];
                Feature f = new Feature();
                FeatureSet fs = new FeatureSet(f.FeatureType);
                for (int i = 0; i < 1; i++)
                {
                    Coordinate[] coord = new Coordinate[dgvData.Rows.Count + 1];
                    for (int j = 0; j < dgvData.Rows.Count; j++)
                    {
                        coord[j] = new Coordinate(Convert.ToDouble(dgvData.Rows[j].Cells[0].Value.ToString()),
                            Convert.ToDouble(dgvData.Rows[j].Cells[1].Value.ToString()));

                    }
                    coord[dgvData.Rows.Count] = new Coordinate(coord[0].X, coord[0].Y);
                    pg[i] = new Polygon(coord);
                    fs.Features.Add(pg[i]);
                }
                fs.SaveAs(fileName, true);
            }
            OpenShapefile(fileName);
        }
        private void cbGeometria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
