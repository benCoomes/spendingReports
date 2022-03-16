using System;
using Coomes.SpendingReports.Api.Categories;

namespace Coomes.SpendingReports.CsvData
{
    internal class ClassifierDataModel
    {
        public string Category { get; set; }
        public string SearchValue { get; set; }

        public Classifier ToDomainModel() 
        {
            return new Classifier(SearchValue, Category);
        }
    }

    internal static class ClassifierExtensisons
    {
        public static ClassifierDataModel ToDataModel(this Classifier domainModel)
        {
            return new ClassifierDataModel
            {
                Category = domainModel.Category,
                SearchValue = domainModel.SearchValue
            };
        }
    }

}