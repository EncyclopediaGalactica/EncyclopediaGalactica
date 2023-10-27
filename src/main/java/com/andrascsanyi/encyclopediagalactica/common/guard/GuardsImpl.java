package com.andrascsanyi.encyclopediagalactica.common.guard;

import org.springframework.stereotype.Service;

/**
 * {@inheritDoc}
 */
@Service
public class GuardsImpl implements Guards {

    /**
     * {@inheritDoc}
     */
    @Override
    public LongGuards LongGuards() {
        return longGuards;
    }

    /**
     * {@inheritDoc}
     *
     * @return
     */
    @Override
    public ObjectGuards ObjectGuards() {
        return objectGuards;
    }

    private final LongGuards longGuards;
    private final ObjectGuards objectGuards;

    public GuardsImpl(
        LongGuards longGuards,
        ObjectGuards objectGuards
    ) {
        this.longGuards = longGuards;
        this.objectGuards = objectGuards;
    }
}
