using System;

namespace TCGCore
{
    public class ConsoleImpl : InputOutputBase
    {
        public override void WriteOutput(string output)
        {
            Console.WriteLine(output);
        }

        public override string GetInput()
        {
            return Console.ReadLine();
        }
    }
}
