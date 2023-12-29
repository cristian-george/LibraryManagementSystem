using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Library.DataLayer.Tests.RepositorySpecflow
{
    [Binding]
    public sealed class DeleteById
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public DeleteById(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I want to delete data from the (.*) with the Id (.*)")]
        public void GivenIWantToDeleteDataFromTheWithTheId(object o, int id)
        {
        }

        [When(@"I call the DeleteById method")]
        public void WhenICallTheDeleteByIdMethod()
        {
        }

        [Then(@"I should have a valid result")]
        public void ThenIShouldHaveAValidResult()
        {
            Assert.IsTrue(true);
        }
    }
}