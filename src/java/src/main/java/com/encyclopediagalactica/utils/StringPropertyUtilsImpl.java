package com.encyclopediagalactica.utils;

import com.encyclopediagalactica.document.model.DocumentEntity;
import org.springframework.stereotype.Service;

@Service
public class StringPropertyUtilsImpl implements StringPropertyUtils {
    @Override
    public void stripStringProperties(DocumentEntity documentEntity) {
        if (documentEntity.getName() != null) {
            documentEntity.setName(documentEntity.getName().strip());
        }
        if (documentEntity.getDesc() != null) {
            documentEntity.setDesc(documentEntity.getDesc().strip());
        }
    }
}
