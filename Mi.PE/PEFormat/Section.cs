﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mi.PE.PEFormat
{
    public sealed class Section
    {
        public const int Size = 40;

        uint m_SizeOfRawData;
        byte[] m_Content;

        const int MaxNameLength = 8;
        string m_Name;

        /// <summary>
        /// An 8-byte, null-padded UTF-8 string.
        /// There is no terminating null character if the string is exactly eight characters long.
        /// For longer names, this member contains a forward slash (/)
        /// followed by an ASCII representation of a decimal number that is an offset into the string table.
        /// Executable images do not use a string table
        /// and do not support section names longer than eight characters.
        /// </summary>
        public string Name
        {
            get { return this.m_Name; }
            set
            {
                if (value != null
                    && value.Length > MaxNameLength)
                    throw new ArgumentException("Too long a name of section.", "value");

                this.m_Name = value;
            }
        }

        /// <summary>
        /// The total size of the section when loaded into memory, in bytes.
        /// If this value is greater than the <see cref="SizeOfRawData"/> member, the section is filled with zeroes.
        /// This field is valid only for executable images and should be set to 0 for object files.
        /// This field overlaps with <see cref="PhysicalAddress"/>.
        /// </summary>
        public uint VirtualSize { get; set; }

        /// <summary>
        /// The file address.
        /// This field overlaps with <see cref="VirtualSize"/>.
        /// </summary>
        public uint PhysicalAddress { get { return this.VirtualSize; } set { this.VirtualSize = value; } }

        /// <summary>
        /// The address of the first byte of the section when loaded into memory, relative to the image base.
        /// For object files, this is the address of the first byte before relocation is applied.
        /// </summary>
        public uint VirtualAddress { get; set; }

        /// <summary>
        /// The size of the initialized data on disk, in bytes.
        /// This value must be a multiple of the <see cref="OptionalHeader.FileAlignment"/> member
        /// of the <see cref="OptionalHeader"/> structure.
        /// If this value is less than the <see cref="VirtualSize"/> member,
        /// the remainder of the section is filled with zeroes.
        /// If the section contains only uninitialized data, the member is zero.
        /// </summary>
        public uint SizeOfRawData
        {
            get { return this.m_SizeOfRawData; }
            set
            {
                if(value == this.SizeOfRawData)
                    return;

                m_SizeOfRawData = value;

                if (m_Content != null
                    && value != m_Content.Length)
                    m_Content = null;
            }
        }

        /// <summary>
        /// A file pointer to the first page within the COFF file.
        /// This value must be a multiple of the <see cref="OptionalHeader.FileAlignment"/> member
        /// of the <see cref="OptionalHeader"/> structure.
        /// If a section contains only uninitialized data, set this member is zero.
        /// </summary>
        public uint PointerToRawData { get; set; }

        /// <summary>
        /// A file pointer to the beginning of the relocation entries for the section.
        /// If there are no relocations, this value is zero.
        /// </summary>
        public uint PointerToRelocations { get; set; }

        /// <summary>
        /// A file pointer to the beginning of the line-number entries for the section.
        /// If there are no COFF line numbers, this value is zero.
        /// </summary>
        public uint PointerToLinenumbers { get; set; }

        /// <summary>
        /// The number of relocation entries for the section.
        /// This value is zero for executable images.
        /// </summary>
        public ushort NumberOfRelocations { get; set; }

        /// <summary>
        /// The number of line-number entries for the section.
        /// </summary>
        public ushort NumberOfLinenumbers { get; set; }

        /// <summary>
        /// The characteristics of the image.
        /// </summary>
        public SectionCharacteristics Characteristics { get; set; }

        public byte[] Content
        {
            get
            {
                if (this.m_Content==null)
                    this.m_Content = new byte[this.SizeOfRawData];

                return m_Content;
            }
        }

        #region ToString
        public override string ToString()
        {
            return
                this.Name +
                " [" + this.PointerToRawData.ToString("X") + ":" + this.SizeOfRawData.ToString("X") + "h" + "]" +
                "=>" +
                "Virtual[" + this.VirtualAddress.ToString("X") + ":" + this.VirtualSize.ToString("X") + "h" + "]";
        }
        #endregion
    }
}