using System.Linq;

partial class CpiTool
{
    static void PrintObject(string header, object obj)
    {
        var fields = obj.GetType().GetFields();
        var nameWidth = fields.Select(field => field.Name.Length).Max();

        foreach (var field in fields)
        {
            System.Console.WriteLine(
                string.Format(
                    "{0}{1,-" + nameWidth.ToString() + "} = {2}",
                    header, field.Name, field.GetValue(obj)));
        }
    }

    static void PrintGlyph(byte[] glyph, int width)
    {
        var rowSize = (width - 1) / 8 + 1;
        for (int r = 0; r < glyph.Length / rowSize; r++)
        {
            for (int c = 0; c < width; c++)
            {
                var bit = (glyph[r * rowSize + c / 8] >> (7 - (c % 8)) & 1) == 1;
                System.Console.Write(bit ? '#' : ' ');
            }
            System.Console.WriteLine();
        }
    }
}
