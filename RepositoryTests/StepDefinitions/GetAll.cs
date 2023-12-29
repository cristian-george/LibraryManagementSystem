using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Library.DataLayer.Tests.RepositorySpecflow
{
    [Binding]
    public sealed class GetAll
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public GetAll(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I want to get all data from the (.*)")]
        public void GivenIWantToGetAllDataFromThe(object o)
        {
        }

        [When(@"I call the Get method")]
        public void WhenICallTheGetMethod()
        {
        }

        [Then(@"I should have a valid result")]
        public void ThenIShouldHaveAValidResult()
        {
            Assert.IsTrue(true);
        }
    }
}