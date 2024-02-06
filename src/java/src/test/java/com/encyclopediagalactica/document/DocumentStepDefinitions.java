package com.encyclopediagalactica.document;

import static org.assertj.core.api.Assertions.assertThat;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import com.encyclopediagalactica.ScenarioContext;
import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.model.DocumentEntity;
import com.encyclopediagalactica.document.scenarios.CreateDocumentScenario;
import com.encyclopediagalactica.document.scenarios.GetDocumentsScenario;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;


@SpringBootTest
public class DocumentStepDefinitions {

    @Autowired
    private GetDocumentsScenario getDocumentsScenario;

    @Autowired
    private CreateDocumentScenario createDocumentScenario;

    @Autowired
    private DocumentTestUtils documentTestUtils;

    @Autowired
    private ScenarioContext scenarioContext;

    @Given("we have {int} documents in the system already")
    public void weHaveGivenDocumentsInTheSystemAlready(int amount) {
        Iterable<Document> testDocuments = documentTestUtils.createDocuments(amount);
        for (Document d : testDocuments) {
            createDocumentScenario.create(d);
        }
    }

    @When("the list of documents are requested")
    public void whenTheListOfDocumentsRequested() {
        List<Document> documentsList = getDocumentsScenario.getAll();
        scenarioContext.add("documentsList", documentsList);
    }

    @Then("the result list length is {int}")
    public void theResultListLengthIsExpected(int expectedLength) {
        List<DocumentEntity> result = (List<DocumentEntity>) scenarioContext.get("documentsList");
        assertThat(result.size()).isEqualTo(expectedLength);
    }
}
