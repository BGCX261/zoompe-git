﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Mi.PE;
using Mi.PE.PEFormat;

namespace Zoom.PE.Model
{
    public sealed class DataDirectoryModel : INotifyPropertyChanged
    {
        readonly OptionalHeader optionalHeader;
        readonly DataDirectoryKind m_Kind;

        public DataDirectoryModel(OptionalHeader optionalHeader, DataDirectoryKind dataDirectoryKind)
        {
            this.optionalHeader = optionalHeader;
            this.m_Kind = dataDirectoryKind;
        }

        public DataDirectoryKind Kind { get { return m_Kind; } }

        public uint VirtualAddress
        {
            get { return optionalHeader.DataDirectories[(int)this.Kind].VirtualAddress; }
            set
            {
                if (value == this.VirtualAddress)
                    return;

                optionalHeader.DataDirectories[(int)this.Kind].VirtualAddress = value;
                OnPropertyChanged("VirtualAddress");
            }
        }

        public uint Size
        {
            get { return optionalHeader.DataDirectories[(int)this.Kind].Size; }
            set
            {
                if (value == this.Size)
                    return;

                optionalHeader.DataDirectories[(int)this.Kind].Size = value;
                OnPropertyChanged("Size");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            var propertyChangedHandler = this.PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
