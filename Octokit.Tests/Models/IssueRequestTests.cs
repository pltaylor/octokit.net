﻿using System;
using System.Globalization;
using Octokit;
using Xunit;

public class IssueRequestTests
{
    public class TheToParametersDictionaryMethod
    {
        [Fact]
        public void OnlyContainsChangedValues()
        {
            var request = new IssueRequest { SortDirection = SortDirection.Ascending };

            var parameters = request.ToParametersDictionary();

            Assert.Equal(1, parameters.Count);
            Assert.Equal("asc", parameters["direction"]);
        }

        [Fact]
        public void ContainsSetValues()
        {
            var request = new IssueRequest
            {
                Filter = IssueFilter.All,
                State = ItemState.Closed,
                SortProperty = IssueSort.Comments,
                SortDirection = SortDirection.Ascending,
                Since = DateTimeOffset.ParseExact("Wed 23 Jan 2013 8:30 AM -08:00",
                    "ddd dd MMM yyyy h:mm tt zzz", CultureInfo.InvariantCulture)
            };
            request.Labels.Add("bug");
            request.Labels.Add("feature");

            var parameters = request.ToParametersDictionary();

            Assert.Equal("all", parameters["filter"]);
            Assert.Equal("closed", parameters["state"]);
            Assert.Equal("comments", parameters["sort"]);
            Assert.Equal("asc", parameters["direction"]);
            Assert.Equal("bug,feature", parameters["labels"]);
            Assert.Equal("2013-01-23T16:30:00Z", parameters["since"]);
        }

        [Fact]
        public void ReturnsEmptyDictionaryForDefaultRequest()
        {
            var request = new IssueRequest();

            var parameters = request.ToParametersDictionary();

            Assert.Empty(parameters);
        }
    }
}
