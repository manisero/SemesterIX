﻿using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing
{
    public interface ITransactionProcessor
    {
        void AppendTransaction(int transactionId, string transaction, DataSetRepresentationBuildState buildState);
    }
}