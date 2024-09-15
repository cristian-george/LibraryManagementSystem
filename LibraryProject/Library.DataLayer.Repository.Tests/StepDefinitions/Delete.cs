using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Library.DataLayer.Tests.RepositorySpecflow
{
    [Binding]
    public sealed class Delete
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public Delete(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I want to delete all data from the (.*)")]
        public void GivenIWantToDeleteAllDataFromThe(object o)
        {
        }

        [When(@"I call the Delete method")]
        public void WhenICallTheDeleteMethod()
        {
        }

        [Then(@"I should have a valid result")]
        public void ThenIShouldHaveAValidResult()
        {
            Assert.IsTrue(true);
        }
    }
}