using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Library.DataLayer.Tests.RepositorySpecflow
{
    [Binding]
    public sealed class Insert
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public Insert(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I want to insert data into the (.*)")]
        public void GivenIWantToInsertDataIntoThe(object o)
        {
        }

        [When(@"I call the Insert method")]
        public void WhenICallTheInsertMethod()
        {
        }

        [Then(@"I should have a valid result")]
        public void ThenIShouldHaveAValidResult(object o)
        {
            Assert.IsTrue(true);
        }
    }
}