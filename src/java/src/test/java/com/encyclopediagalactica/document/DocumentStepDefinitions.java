package com.encyclopediagalactica.document;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.ctx.ScenarioContext;
import com.encyclopediagalactica.document.model.DocumentEntity;
import com.encyclopediagalactica.document.scenarios.CreateDocumentScenario;
import com.encyclopediagalactica.document.scenarios.GetDocumentsScenario;
import com.encyclopediagalactica.utils.DocumentTestUtilsImp;
import com.encyclopediagalactica.utils.InputStringUtils;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;


public class DocumentStepDefinitions {

    private static final String CREATE_DOCUMENT_EXCEPTION = "createDocumentException";
    private static final String CREATE_DOCUMENT_RESULT = "createdDocumentResult";
    @Autowired
    private GetDocumentsScenario getDocumentsScenario;

    @Autowired
    private CreateDocumentScenario createDocumentScenario;

    @Autowired
    private DocumentTestUtilsImp documentTestUtilsImp;

    @Autowired
    private ScenarioContext scenarioContext;

    @Autowired
    private InputStringUtils inputStringUtils;

    private final static String DOCUMENT_OBJECT = "documentObject";
    private final static String DOCUMENT_LIST = "documentList";

    @Given("we have {int} documents in the system already")
    public void weHaveGivenDocumentsInTheSystemAlready(int amount) {
        Iterable<Document> testDocuments = documentTestUtilsImp.createDocuments(amount, true);
        for (Document d : testDocuments) {
            createDocumentScenario.create(d);
        }
    }

    @When("the list of documents are requested")
    public void whenTheListOfDocumentsRequested() {
        List<Document> documentsList = getDocumentsScenario.getAll();
        scenarioContext.add(DOCUMENT_LIST, documentsList);
    }

    @Then("the result list length is {int}")
    public void theResultListLengthIsExpected(int expectedLength) {
        List<DocumentEntity> result = (List<DocumentEntity>) scenarioContext.get(DOCUMENT_LIST);
        assertThat(result.size()).isEqualTo(expectedLength);
    }

    @Given("there is a Document entity")
    public void thereIsADocumentEntity() {
        scenarioContext.add(DOCUMENT_OBJECT, new Document.Builder());
    }

    @And("the id is {int}")
    public void theIdIsId(Integer id) {
        Document.Builder builder = (Document.Builder) scenarioContext.get(DOCUMENT_OBJECT);
        builder.setId(id.toString());
        scenarioContext.add(DOCUMENT_OBJECT, builder);
    }

    @And("the name is {word}")
    public void theNameIsName(String name) {
        Document.Builder builder = (Document.Builder) scenarioContext.get(DOCUMENT_OBJECT);
        String modifiedValue = inputStringUtils.provideValue(name);
        builder.setName(modifiedValue);
        scenarioContext.add(DOCUMENT_OBJECT, builder);
    }

    @And("the description is {word}")
    public void theDescriptionIsDescription(String description) {
        Document.Builder builder = (Document.Builder) scenarioContext.get(DOCUMENT_OBJECT);
        String modifiedValue = inputStringUtils.provideValue(description);
        builder.setDesc(modifiedValue);
        scenarioContext.add(DOCUMENT_OBJECT, builder);
    }

    @When("the Document is created")
    public void theDocumentIsCreated() {
        Document.Builder builder = (Document.Builder) scenarioContext.get(DOCUMENT_OBJECT);
        Document result = builder.build();
        Document createdDocument = null;
        try {
            createdDocument = createDocumentScenario.create(result);
            scenarioContext.add(CREATE_DOCUMENT_RESULT, createdDocument);
        } catch (Exception e) {
            scenarioContext.add(CREATE_DOCUMENT_EXCEPTION, e);
        }
    }

    @Then("{string} message returned")
    public void errorMessageReturned(String errorMessage) {
        Exception exception = (Exception) scenarioContext.get(CREATE_DOCUMENT_EXCEPTION);
        String message = exception.getMessage();
        assertThat(message).isEqualTo(errorMessage);
    }
}
