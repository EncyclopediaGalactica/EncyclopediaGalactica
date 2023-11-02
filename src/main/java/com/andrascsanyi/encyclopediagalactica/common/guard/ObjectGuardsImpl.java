package com.andrascsanyi.encyclopediagalactica.common.guard;

import com.andrascsanyi.encyclopediagalactica.common.guard.exceptions.ObjectIsNullException;
import org.springframework.stereotype.Service;

@Service
public class ObjectGuardsImpl implements ObjectGuards {

    @Override
    public boolean isNotNull(Object o) {
        return o != null;
    }

    @Override
    public void throwIfNull(Object o) {
        if (o == null) {
            String message = "The provided Object must not be null.";
            throw new ObjectIsNullException(message);
        }
    }
}
