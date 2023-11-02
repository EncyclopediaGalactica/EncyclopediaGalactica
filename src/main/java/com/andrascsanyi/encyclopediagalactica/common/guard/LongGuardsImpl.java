package com.andrascsanyi.encyclopediagalactica.common.guard;

import java.util.Objects;
import org.springframework.stereotype.Service;

@Service
public class LongGuardsImpl implements LongGuards {

    @Override
    public boolean areNotEquals(Long l1, Long l2) {
        return !Objects.equals(l1, l2);
    }

    @Override
    public boolean areNotEquals(long l1, long l2) {
        return l1 != l2;
    }
}
