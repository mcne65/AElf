﻿using AElf.Database;
using Google.Protobuf;

// ReSharper disable once CheckNamespace
namespace AElf.Kernel
{
    public partial class SmartContractDeployment : ISerializable
    {
        public byte[] Serialize()
        {
            return this.ToByteArray();
        }
    }
}