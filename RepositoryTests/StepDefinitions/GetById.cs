using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Library.DataLayer.Tests.RepositorySpecflow
{
    [Binding]
    public sealed class GetById
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public GetById(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I want to get data from the (.*) with the Id (.*)")]
        public void GivenIWantToGetDataFromTheWithTheId(object o, int id)
        {
        }

        [When(@"I call the GetByID method")]
        public void WhenICallTheGetByIDMethod()
        {
        }

        [Then(@"I should have a valid result")]
        public void ThenIShouldHaveAValidResult()
        {
            Assert.IsTrue(true);
        }
    }
}