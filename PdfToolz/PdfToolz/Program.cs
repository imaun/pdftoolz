// See https://aka.ms/new-console-template for more information

using PdfToolz.Tools;

Console.WriteLine("Hello, World!");
Console.WriteLine("PdfToolz v0.0.1");
Console.WriteLine("You can extract text from PDF files using this tool.");

string filename = "";
if (args.Any())
    filename = args[0];
else
{
    Console.Write("Please enter Pdf file path : ");
    filename = Console.ReadLine();
    Console.WriteLine();
}

try
{
    var result = TextExtractor.ExtractText(filename);
    if (result.Any())
    {
        Console.WriteLine($"Found {result.Count} lines of text! Enter a key to see the list.");
        Console.ReadKey();
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
    }
}
catch (ArgumentNullException)
{
    Console.WriteLine("ERROR : Please enter a PDF filename as the argument!");
}
catch (FileNotFoundException)
{
    Console.WriteLine($"ERROR : FileNotFound. Filename : '{filename}'");
}

Console.WriteLine("Finished! Enter any key to exit...");
