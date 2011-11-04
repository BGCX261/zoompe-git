﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mi.PE;
using Mi.PE.Internal;
using Mi.PE.PEFormat;

namespace PrintExports
{
    class Program
    {
        static void Main(string[] args)
        {
            string kernel32 = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.System),
                "kernel32.dll");

            Console.WriteLine(Path.GetFileName(kernel32));
            var exports = GetExportFor(kernel32);

            Console.WriteLine(exports.DllName + " " + exports.Timestamp);
            foreach (var i in exports.Exports)
            {
                Console.WriteLine("  " + i.ToString());
            }

            //string self = typeof(Program).Assembly.Location;
            //Console.WriteLine(Path.GetFileName(self));
            //exports = GetExportFor(self);

            //foreach (var i in exports)
            //{
            //    Console.WriteLine("  " + i.ToString());
            //}

        }

        private static Mi.PE.Unmanaged.Export.Header GetExportFor(string file)
        {
            var stream = new MemoryStream(File.ReadAllBytes(file));
            var reader = new BinaryStreamReader(stream, new byte[1024]);
            var pe = new PEFile();
            pe.ReadFrom(reader);

            var exportDirectory = pe.OptionalHeader.DataDirectories[(int)DataDirectoryKind.ExportSymbols];

            var rvaStream = new RvaStream(
                stream,
                pe.SectionHeaders.Select(
                s => new RvaStream.Range
                {
                    PhysicalAddress = s.PointerToRawData,
                    Size = s.VirtualSize,
                    VirtualAddress = s.VirtualAddress
                })
                .ToArray());

            rvaStream.Position = exportDirectory.VirtualAddress;

            var sectionReader = new BinaryStreamReader(rvaStream, new byte[32]);

            var exports = new Mi.PE.Unmanaged.Export.Header();
            exports.ReadExports(sectionReader, 0);
            return exports;
        }
    }
}