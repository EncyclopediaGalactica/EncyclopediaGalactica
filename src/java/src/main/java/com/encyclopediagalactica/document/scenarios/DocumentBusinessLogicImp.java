package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.dto.DocumentDto;
import com.encyclopediagalactica.document.entities.DocumentEntity;
import com.encyclopediagalactica.document.mappers.DocumentMapper;
import com.encyclopediagalactica.document.repositories.DocumentRepository;
import com.encyclopediagalactica.document.validation.DocumentValidation;
import lombok.NonNull;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.StreamSupport;

@Service
public class DocumentBusinessLogicImp implements DocumentBusinessLogic {
    private final DocumentRepository documentRepository;
    private final DocumentMapper documentMapper;
    private final DocumentValidation documentValidation;

    public DocumentBusinessLogicImp(
            @NonNull DocumentRepository documentRepository,
            @NonNull DocumentMapper documentMapper,
            @NonNull DocumentValidation documentValidation) {
        this.documentRepository = documentRepository;
        this.documentMapper = documentMapper;
        this.documentValidation = documentValidation;
    }

    @Override
    public List<DocumentDto> getDocuments() {
        List<DocumentEntity> documentEntityEntities = StreamSupport.stream(
                        documentRepository.findAll().spliterator(), false)
                .toList();
        List<DocumentDto> documentDtos = documentMapper.mapDocumentsToDocumentDtos(documentEntityEntities);
        return documentDtos;
    }

    @Override
    public DocumentDto getDocument(Long id) {
        DocumentEntity documentEntity = documentRepository.findById(id).get();
        DocumentDto result = documentMapper.mapDocumentToDocumentDto(documentEntity);
        return result;
    }

    @Override
    public DocumentDto createDocument(DocumentDto documentDto) {
        documentValidation.validateCreateNewDocumentScenario(documentDto);
        DocumentEntity documentEntity = documentMapper.mapDocumentDtoToDocument(documentDto);
        DocumentEntity result = documentRepository.save(documentEntity);
        DocumentDto resultDto = documentMapper.mapDocumentToDocumentDto(result);
        return resultDto;
    }

    @Override
    public DocumentDto modifyDocument(Long documentId, DocumentDto documentDto) {
        documentValidation.validateModifyDocumentScenario(documentDto);
        DocumentEntity exampleModifiedDocumentEntityEntity = documentMapper.mapDocumentDtoToDocument(documentDto);
        DocumentEntity toBeModifiedDocumentEntityEntity = documentRepository.findById(documentId).orElseThrow();

        modifyDocumentUpdateFields(toBeModifiedDocumentEntityEntity, exampleModifiedDocumentEntityEntity);

        DocumentEntity result = documentRepository.save(toBeModifiedDocumentEntityEntity);
        DocumentDto resultDto = documentMapper.mapDocumentToDocumentDto(result);
        return resultDto;
    }

    private static void modifyDocumentUpdateFields(DocumentEntity toBeModifiedDocumentEntityEntity, DocumentEntity exampleModifiedDocumentEntityEntity) {
        if (!toBeModifiedDocumentEntityEntity.getName().equals(exampleModifiedDocumentEntityEntity.getName())) {
            toBeModifiedDocumentEntityEntity.setName(exampleModifiedDocumentEntityEntity.getName());
        }

        if (!toBeModifiedDocumentEntityEntity.getDesc().equals(exampleModifiedDocumentEntityEntity.getDesc())) {
            toBeModifiedDocumentEntityEntity.setDesc(exampleModifiedDocumentEntityEntity.getDesc());
        }
    }
}
