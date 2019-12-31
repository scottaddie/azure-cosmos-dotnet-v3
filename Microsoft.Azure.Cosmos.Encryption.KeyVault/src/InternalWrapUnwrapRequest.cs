//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------
namespace Microsoft.Azure.Cosmos.Encryption.KeyVault
{
    internal sealed class InternalWrapUnwrapRequest
    {
        public string Alg { get; set; }

        public string Value { get; set; }
    }
}