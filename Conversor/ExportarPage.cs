using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DotSpatial.Data;
using DotSpatial.Topology;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conversor
{
    public partial class ExportarPage : Form
    {
        NpgsqlConnection conexao = new NpgsqlConnection(
            "Server=127.0.0.1;Port=5432; User Id=postgres; Password=postgres;" +
            "Database=db_aula;");

        IList<Tabela> Geometrias = new List<Tabela>();
        IList<Ponto> Pontos = new List<Ponto>();
        IList<string> Srids = new List<string>();
        public ExportarPage()
        {
            InitializeComponent();
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

        private void cbGeometria_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvGeometrias.ClearSelection();
            dgvGeometrias.DataSource = null;
            dgvPontos.ClearSelection();
            dgvPontos.DataSource = null;
            Geometrias.Clear();
            Pontos.Clear();

            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;

                cmd.CommandText = "SELECT tabelanome FROM tb_tabelatipo WHERE tipo = @index";
                cmd.Parameters.AddWithValue("@index", cbGeometria.SelectedIndex);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tabela = new Tabela
                        {
                            Nome = reader.GetString(0)
                        };
                        Geometrias.Add(tabela);
                    }
                }
            }
            conexao.Close();
            dgvGeometrias.DataSource = Geometrias;
        }

        private void dgvGeometrias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvGeometrias_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void ConvertTextToPoligono(string poligono)
        {
            string geometria = poligono.Replace("POLYGON((", "").Replace("))", "");
            var ponto = geometria.Split(',');
            for (int i = 0; i < ponto.Length; i++)
            {
                var pos = ponto[i].Split(' ');
                var Ponto = new Ponto()
                {
                    PosX = (float)Convert.ToDouble(pos[0]),
                    PosY = (float)Convert.ToDouble(pos[1])
                };
                Pontos.Add(Ponto);
            }
            Pontos.Remove(Pontos[Pontos.Count - 1]);
        }

        private void ConvertTextToLinha(string linha)
        {
            string geometria = linha.Replace("LINESTRING(", "").Replace(")", "");
            var ponto = geometria.Split(',');
            for (int i = 0; i < ponto.Length; i++)
            {
                var pos = ponto[i].Split(' ');
                var Ponto = new Ponto()
                {
                    PosX = (float)Convert.ToDouble(pos[0]),
                    PosY = (float)Convert.ToDouble(pos[1])
                };
                Pontos.Add(Ponto);
            }
        }

        private void dgvGeometrias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPontos.ClearSelection();
            dgvPontos.DataSource = null;
            Pontos.Clear();

            string[] dados = cbSrid.SelectedItem.ToString().Split(' ');
            string tipogeo = dados[0];

            string tabelaNome = dgvGeometrias.CurrentRow.Cells[0].Value.ToString();
            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;

                //Ponto
                if (cbGeometria.SelectedIndex == 0)
                {
                    cmd.CommandText = "SELECT st_x(st_transform(the_geom," + tipogeo + "))," +
                        "st_y(st_transform(the_geom," + tipogeo + ")) FROM " + tabelaNome;
                }
                else
                {
                    cmd.CommandText = "SELECT st_astext(st_transform(the_geom," + tipogeo + ")) FROM " + tabelaNome;
                }


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Ponto
                        if (cbGeometria.SelectedIndex == 0)
                        {
                            var ponto = new Ponto()
                            {
                                PosX = (float)reader.GetDouble(0),
                                PosY = (float)reader.GetDouble(1)
                            };
                            Pontos.Add(ponto);
                        }

                        //Linha
                        if (cbGeometria.SelectedIndex == 1)
                        {
                            ConvertTextToLinha(reader.GetString(0));
                        }

                        //Poligono
                        if (cbGeometria.SelectedIndex == 2)
                        {
                            ConvertTextToPoligono(reader.GetString(0));
                        }
                    }
                }
            }
            conexao.Close();
            dgvPontos.DataSource = Pontos;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rbTxt.Checked)
            {
                saveFileDialog1.DefaultExt = null;
                saveFileDialog1.DefaultExt = "txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    GerarArquivoTxt();
                }
            }
            if (rbXls.Checked)
            {
                saveFileDialog1.DefaultExt = null;
                saveFileDialog1.DefaultExt = "xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    GerarArquivoXls();
                }
            }

            if (rbShp.Checked)
            {
                saveFileDialog1.DefaultExt = null;
                saveFileDialog1.DefaultExt = "shp";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    GerarArquivoShp();
                }
            }
            if (rbKml.Checked)
            {
                saveFileDialog1.DefaultExt = null;
                saveFileDialog1.DefaultExt = "kml";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    GerarArquivoKml();
                }
            }
        }

        private void GerarArquivoKml()
        {
            string[] dados = cbSrid.SelectedItem.ToString().Split(' ');
            string tipogeo = dados[0];
            String output;
            string tabelaNome = dgvGeometrias.CurrentRow.Cells[0].Value.ToString();
            conexao.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conexao;

                cmd.CommandText = "SELECT ST_AsKML(the_geom, " + tipogeo + ") from " + tabelaNome;
                output = "<?xml version=\"1.0\" encoding=\"UTF-8\"?> \n"
                        + "<kml xmlns=\"http://www.opengis.net/kml/2.2\"> \n"
                        + "<Document>\n"
                        + "<Placemark> \n"
                        + "<description><![CDATA[<table><tr><td><b>Teste!</b></td></tr></table>]]></description>";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        output += reader.GetString(0);
                    }
                    output += "</Placemark>\n"
                        + "</Document>\n"
                        + "</kml>";
                }
            }
            conexao.Close();
            StreamWriter wr = new StreamWriter(saveFileDialog1.FileName, false);
            /*for (int j = 0; j < dgvPontos.Rows.Count; j++)
            {
                wr.WriteLine(dgvPontos.Rows[j].Cells[0].Value.ToString() + ";" +
                dgvPontos.Rows[j].Cells[1].Value.ToString());
            }*/
            wr.WriteLine(output);
            wr.Close();
        }

        private void GerarArquivoShp()
        {
            //ponto
            if (cbGeometria.SelectedIndex == 0)
            {
                Coordinate[] c = new Coordinate[dgvPontos.Rows.Count];
                Feature f = new Feature();
                FeatureSet fs = new FeatureSet(f.FeatureType);

                for (int j = 0; j < dgvPontos.Rows.Count; j++)
                {
                    c[j] = new Coordinate(Convert.ToDouble(dgvPontos.Rows[j].Cells[0].Value.ToString()),
                        Convert.ToDouble(dgvPontos.Rows[j].Cells[1].Value.ToString()));
                    fs.Features.Add(c[j]);
                }
                fs.SaveAs(saveFileDialog1.FileName, true);
            }

            //linha
            if (cbGeometria.SelectedIndex == 1)
            {
                Feature f = new Feature();
                FeatureSet fs = new FeatureSet(f.FeatureType);
                for (int ii = 0; ii < 1; ii++)
                {
                    Coordinate[] coord = new Coordinate[dgvPontos.Rows.Count];
                    for (int j = 0; j < dgvPontos.Rows.Count; j++)
                    {
                        coord[j] = new Coordinate(Convert.ToDouble(dgvPontos.Rows[j].Cells[0].Value.ToString()),
                            Convert.ToDouble(dgvPontos.Rows[j].Cells[1].Value.ToString()));
                    }
                    LineString ls = new LineString(coord);
                    f = new Feature(ls);
                    fs.Features.Add(f);
                }
                fs.SaveAs(saveFileDialog1.FileName, true);
            }

            //poligono
            if (cbGeometria.SelectedIndex == 2)
            {
                Polygon[] pg = new Polygon[1];
                Feature f = new Feature();
                FeatureSet fs = new FeatureSet(f.FeatureType);
                for (int i = 0; i < 1; i++)
                {
                    Coordinate[] coord = new Coordinate[dgvPontos.Rows.Count + 1];
                    for (int j = 0; j < dgvPontos.Rows.Count; j++)
                    {
                        coord[j] = new Coordinate(Convert.ToDouble(dgvPontos.Rows[j].Cells[0].Value.ToString()),
                            Convert.ToDouble(dgvPontos.Rows[j].Cells[1].Value.ToString()));

                    }
                    coord[dgvPontos.Rows.Count] = new Coordinate(coord[0].X, coord[0].Y);
                    pg[i] = new Polygon(coord);
                    fs.Features.Add(pg[i]);
                }
                fs.SaveAs(saveFileDialog1.FileName, true);
            }
        }

        private void GerarArquivoTxt()
        {

            StreamWriter wr = new StreamWriter(saveFileDialog1.FileName, false);
            for (int j = 0; j < dgvPontos.Rows.Count; j++)
            {
                wr.WriteLine(dgvPontos.Rows[j].Cells[0].Value.ToString() + ";" +
                dgvPontos.Rows[j].Cells[1].Value.ToString());
            }
            wr.Close();
        }
        private void GerarArquivoXls()
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(saveFileDialog1.FileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "mySheet" };
                sheets.Append(sheet);

                // Get the sheetData cell table.
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                uint value = 1;
                for (int j = 0; j < dgvPontos.Rows.Count; j++)
                {
                    // Add a row to the cell table.
                    Row row;
                    row = new Row() { RowIndex = value };
                    sheetData.Append(row);

                    var cell = new Cell
                    {
                        CellValue = new CellValue(dgvPontos.Rows[j].Cells[0].Value.ToString()),
                        DataType = new EnumValue<CellValues>(CellValues.Number)
                    };

                    row.InsertAt(cell, 0);
                    var cell2 = new Cell
                    {
                        CellValue = new CellValue(dgvPontos.Rows[j].Cells[1].Value.ToString()),
                        DataType = new EnumValue<CellValues>(CellValues.Number)
                    };
                    value++;
                    row.InsertAt(cell2, 1);
                }

                // Close the document.
                spreadsheetDocument.Close();
            }
        }
    }
}
