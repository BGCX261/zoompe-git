﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Mi.PE.Cli.Tables
{
    /// <summary>
    /// [ECMA-335 §23.1.5]
    /// </summary>
    [Flags]
    public enum FieldAttributes : ushort
    {
        /// <summary>
        /// These 3 bits contain one of the following values:
        /// <see cref="CompilerControlled"/>, <see cref="Private"/>,
        /// <see cref="FamANDAssem"/>, <see cref="Assembly"/>,
        /// <see cref="Family"/>, <see cref="FamORAssem"/>,
        /// <see cref="Public"/>.
        /// </summary>
        FieldAccessMask = 0x0007,

        /// <summary>
        /// Member not referenceable.
        /// </summary>
        CompilerControlled = 0x0000,

        /// <summary>
        /// Accessible only by the parent type.
        /// </summary>
        Private = 0x0001,
        
        /// <summary>
        /// Accessible by sub-types only in this Assembly.
        /// </summary>
        FamANDAssem = 0x0002,

        /// <summary>
        /// Accessibly by anyone in the Assembly.
        /// </summary>
        Assembly = 0x0003,

        /// <summary>
        /// Accessible only by type and sub-types.
        /// </summary>
        Family = 0x0004,
        
        /// <summary>
        /// Accessibly by sub-types anywhere, plus anyone in assembly.
        /// </summary>
        FamORAssem = 0x0005,

        /// <summary>
        /// Accessibly by anyone who has visibility to this scope field contract attributes.
        /// </summary>
        Public = 0x0006,


        /// <summary>
        /// Defined on type, else per instance.
        /// </summary>
        Static = 0x0010,
        
        /// <summary>
        /// Field can only be initialized, not written to after init.
        /// </summary>
        InitOnly = 0x0020,

        /// <summary>
        /// Value is compile time constant.
        /// </summary>
        Literal = 0x0040,
        
        /// <summary>
        /// Reserved (to indicate this field should not be serialized when type is remoted).
        /// </summary>
        NotSerialized = 0x0080,
        
        /// <summary>
        /// Field is special.
        /// </summary>
        SpecialName = 0x0200,

        // Interop Attributes

        /// <summary>
        /// Implementation is forwarded through PInvoke.
        /// </summary>
        PInvokeImpl = 0x2000,


        // Additional flags
        
        /// <summary>
        /// CLI provides 'special' behavior, depending upon the name of the field.
        /// </summary>
        RTSpecialName = 0x0400,
        
        /// <summary>
        /// Field has marshalling information.
        /// </summary>
        HasFieldMarshal = 0x1000,

        /// <summary>
        /// Field has default.
        /// </summary>
        HasDefault = 0x8000,
        
        /// <summary>
        /// Field has RVA.
        /// </summary>
        HasFieldRVA = 0x0100
    }
}