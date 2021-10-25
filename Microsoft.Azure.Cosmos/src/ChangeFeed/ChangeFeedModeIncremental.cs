﻿// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

namespace Microsoft.Azure.Cosmos.ChangeFeed
{
    using System;
    using System.Threading;
    using Microsoft.Azure.Cosmos.ChangeFeed.Pagination;
    using Microsoft.Azure.Documents;
    using ChangeFeedPagination = Microsoft.Azure.Cosmos.ChangeFeed.Pagination;
    using CosmosPagination = Microsoft.Azure.Cosmos.Pagination;

    internal sealed class ChangeFeedModeIncremental : ChangeFeedMode
    {
        public static ChangeFeedMode Instance { get; } = new ChangeFeedModeIncremental();

        internal override void Accept(RequestMessage requestMessage)
        {
            requestMessage.Headers.Add(HttpConstants.HttpHeaders.A_IM, HttpConstants.A_IMHeaderValues.IncrementalFeed);
        }

        internal override CosmosPagination.ISplitStrategy<ChangeFeedPagination.ChangeFeedPage, ChangeFeedPagination.ChangeFeedState>
        CreateSplitStrategy(
            CosmosPagination.IFeedRangeProvider feedRangeProvider,
            ChangeFeedPagination.IChangeFeedDataSource dataSource,
            ChangeFeedPagination.ChangeFeedPaginationOptions paginationOptions,
            CancellationToken cancellationToken)
        {
            return new CosmosPagination.DefaultSplitStrategy<ChangeFeedPagination.ChangeFeedPage, ChangeFeedPagination.ChangeFeedState>(
                feedRangeProvider,
                ChangeFeedPartitionRangePageAsyncEnumerator.MakeCreateFunction(dataSource, paginationOptions, cancellationToken));
        }
    }
}
