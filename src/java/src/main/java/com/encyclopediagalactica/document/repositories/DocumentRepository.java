package com.encyclopediagalactica.document.repositories;

import com.encyclopediagalactica.document.entities.DocumentEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface DocumentRepository extends JpaRepository<DocumentEntity, Long> {
}
