package com.encyclopediagalactica.document.model;

import com.encyclopediagalactica.document.infra.validation.CreateDocumentScenarioValidation;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Entity
@Table(name = "document")
public class DocumentEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    @Max(value = 0, groups = {CreateDocumentScenarioValidation.class})
    private Long id;

    @Column(name = "name")
    @NotNull(groups = {CreateDocumentScenarioValidation.class})
    @NotEmpty(groups = {CreateDocumentScenarioValidation.class})
    @Size(min = 3, max = 255, groups = {CreateDocumentScenarioValidation.class})
    private String name;

    @Column(name = "desc")
    @NotNull(groups = {CreateDocumentScenarioValidation.class})
    @NotEmpty(groups = {CreateDocumentScenarioValidation.class})
    @Size(min = 3, max = 255, groups = {CreateDocumentScenarioValidation.class})
    private String desc;
}
