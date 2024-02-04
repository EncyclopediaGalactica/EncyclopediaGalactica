package com.encyclopediagalactica.document.model;

import com.encyclopediagalactica.document.infra.validation.CreateDocumentScenarioValidation;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.Range;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "document")
public class DocumentEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    @Range(min = 0L, max = 0L, groups = {CreateDocumentScenarioValidation.class})
    private Long id;

    @Column(name = "name")
    @Length(min = 3, max = 255, groups = {CreateDocumentScenarioValidation.class})
    private String name;

    @Column(name = "desc")
    @Length(min = 3, max = 255, groups = {CreateDocumentScenarioValidation.class})
    private String desc;
}
