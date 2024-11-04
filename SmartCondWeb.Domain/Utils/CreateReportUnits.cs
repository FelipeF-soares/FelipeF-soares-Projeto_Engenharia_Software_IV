using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.xmp.impl;
using Microsoft.AspNetCore.Hosting;
using SmartCondWeb.Domain.People;
using SmartCondWeb.Domain.Things;
using SmartCondWeb.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.Domain.Utils;

public class CreateReportUnits
{
    private BaseFont baseFont;
    private Font cellFont;
    private string wwwRootPath;
    private string path;
    
    public CreateReportUnits(string wwwRootPath)
    {
        this.wwwRootPath = wwwRootPath + "\\report\\Units\\";
        baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252,false);
        cellFont = new Font(baseFont,10,iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        
    }

    public string ReportPDF(List<Unit> units)
    {
            ClearDirectory(wwwRootPath);
            var pdf = PageConfiguration(15F, 15F, 15F, 20F, true);
            var reportName = CreateReportName("Proprietarios");
            var folder = CreateReportInPath(reportName);
            var writer = CreatePdfWriter(pdf, folder);
            pdf.Open();
            var title = MakeTitle("Relatório dos Proprietários", 32);
            pdf.Add(title);
            string logoPath = wwwRootPath + @"\images\logo\logo.png";
            AddLogo(logoPath, pdf, writer);
            if (units.Any())
            {
                var table = MakeTable(6);
                string[] valueList = { "Bloco", "Apartamento", "Nome", "CPF/CNPJ", "e-mail", "Contato" };
                LikedCellInTable(table, valueList);
                LinkedValueInCell(table, units);
                pdf.Add(table);

            }
            else
            {
                var paragraf = MakeParagraph("Lista vazia");
                pdf.Add(paragraf);
            }
            pdf.Close();
            folder.Close();
        return wwwRootPath + reportName;
  
        return null;
    }

    private Document PageConfiguration(float leftSide, float rightSide, float top, float botom, bool horizontal)
    {
        var pxForMm = 72 / 25.2F;
        top = top * pxForMm;
        botom = botom * pxForMm;
        leftSide = leftSide * pxForMm;
        rightSide = rightSide * pxForMm;

        if(horizontal)
        {
            return new Document(PageSize.A4.Rotate(), leftSide, rightSide, top, botom);
            
        }
        return new Document(PageSize.A4, leftSide, rightSide, top, botom);
    }

    private string CreateReportName(string reportName)
    {
        return $"{reportName}.{DateTime.Now.ToString("dd.MM.yyyy.HH.mm.ss")}.pdf";
    }
    private FileStream CreateReportInPath(string reportName)
    {

        path = wwwRootPath + reportName;
        return new FileStream(path, FileMode.Create);
    }
    private PdfWriter CreatePdfWriter(Document pdf, FileStream path)
    {
        return PdfWriter.GetInstance(pdf, path);
    }
    
    private Paragraph MakeTitle(string title, int fontSize)
    {
        var fontTitle = new Font(baseFont, fontSize, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        var paragraph = new Paragraph($"{title}\n\n", fontTitle);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingAfter = 4;
        return paragraph;
    }
    private Paragraph MakeParagraph(string content)
    {
        var fontTitle = new Font(baseFont, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        var paragraph = new Paragraph($"{content}", fontTitle);
        paragraph.Alignment = Element.ALIGN_LEFT;
        paragraph.SpacingAfter = 4;
        return paragraph;
    }
    private PdfPTable MakeTable(int cols)
    {
        var table = new PdfPTable(cols);
        float[] widhtCol = { 1.5f, 1.5f, 2.5f, 1.5f, 1.5f, 1f };
        table.SetWidths(widhtCol);
        table.DefaultCell.BorderWidth = 0;
        table.WidthPercentage = 100;
        return table;
    }

    private PdfPCell MakeCell(string name, PdfPTable table)
    {
        var bgColor = iTextSharp.text.BaseColor.WHITE;
        if(table.Rows.Count %2 == 1)
        {
            bgColor = new BaseColor(0.95f, 0.95f, 0.95f);
        }
        var cell = new PdfPCell(new Phrase(name,cellFont));
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        cell.Border = 0;
        cell.BorderWidthBottom = 1;
        cell.FixedHeight = 25;
        cell.PaddingBottom = 5;
        cell.BackgroundColor = bgColor;
        return cell;
    }

    private void LikedCellInTable(PdfPTable table, string[] valueList)
    {
        foreach(var value in valueList)
        {
            var cell = MakeCell(value, table);
            table.AddCell(cell);
        }
    }

    private void LinkedValueInCell(PdfPTable table, List<Unit> valueList)
    {

        for(int i = 0; i < valueList.Count; i++)
        {
            var building = MakeCell(valueList[i].Building, table);
            table.AddCell(building);
            var unitNumber = MakeCell(valueList[i].UnitNumber, table);
            table.AddCell(unitNumber);
            if(valueList[i].Homeowner != null)
            {
                var name = MakeCell(valueList[i].Homeowner.Name, table);
                table.AddCell(name);
                var identificationDocument = MakeCell(valueList[i].Homeowner.IdentificationDocument, table);
                table.AddCell(identificationDocument);
                var email = MakeCell(valueList[i].Homeowner.Email, table);
                table.AddCell(email);
                var cellPhone = MakeCell(valueList[i].Homeowner.CellPhone, table);
                table.AddCell(cellPhone);
            }
            else
            {
                var name = MakeCell("Não Cadastrado", table);
                table.AddCell(name);
                var identificationDocument = MakeCell("Não Cadastrado", table);
                table.AddCell(identificationDocument);
                var email = MakeCell("Não Cadastrado", table);
                table.AddCell(email);
                var cellPhone = MakeCell("Não Cadastrado", table);
                table.AddCell(cellPhone);
            }
           
            
           
        }
    }

    private void AddLogo(string logoPath, Document pdf, PdfWriter writer)
    {
        if(File.Exists(logoPath))
        {
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
            float widthForHeight = logo.Width / logo.Height;
            float heightLogo = 90;
            float widthLogo = heightLogo * widthForHeight;
            logo.ScaleToFit(widthLogo, heightLogo);
            var marginLeft = pdf.PageSize.Width - pdf.RightMargin - widthLogo;
            var marginTop = pdf.PageSize.Height - pdf.TopMargin - 54;
            logo.SetAbsolutePosition(marginLeft, marginTop);
            writer.DirectContent.AddImage(logo);
        }
    }
    private void ClearDirectory(string pathDirectory)
    {
        string[] aquivos = Directory.GetFiles(pathDirectory);
       if(aquivos.Length > 0)
        {
            foreach (string aquivo in aquivos)
            {
                File.Delete(aquivo);
            }
        }

    }
}
