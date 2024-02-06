package com.encyclopediagalactica.document.infra.repositories;

import com.encyclopediagalactica.document.model.DocumentEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface DocumentRepository extends JpaRepository<DocumentEntity, Long> {
}
