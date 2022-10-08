using iTextSharp.text.pdf;

namespace PdfToolz.Tools;

public static class TextExtractor
{

    public static byte[] FileToByteArray(string fileName)
    {
        byte[] fileData = null;

        using (FileStream fs = File.OpenRead(fileName)) 
        { 
            using (BinaryReader binaryReader = new BinaryReader(fs))
            {
                fileData = binaryReader.ReadBytes((int)fs.Length); 
            }
        }
        return fileData;
    }
    
    public static IReadOnlyCollection<string> ExtractText(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentNullException(nameof(filename));

        if (!File.Exists(filename))
            throw new FileNotFoundException();
        
        var result = new List<string>();
        
        var bytes = FileToByteArray(filename);
        var pdfReader = new PdfReader(bytes);

        for (int pageNum = 1; pageNum <= pdfReader.NumberOfPages; pageNum++)
        {
            var pageContent = pdfReader.GetPageContent(pageNum);
            var tokenizer = new PrTokeniser(new RandomAccessFileOrArray(pageContent));

            var mylist = new List<string>();
            while (tokenizer.NextToken())
            {
                if(tokenizer.TokenType == PrTokeniser.TK_STRING)
                    mylist.Add(tokenizer.StringValue);
            }

            var text = string.Join(Environment.NewLine, mylist);
            result.Add(text);
        }

        return result;
    }
}