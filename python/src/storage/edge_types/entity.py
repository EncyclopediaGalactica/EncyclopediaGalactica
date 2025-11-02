from sqlalchemy import Column, String, BIGINT, Identity
from sqlalchemy.orm import Mapped
from storage.db import EGBase


class EdgeType(EGBase):
    __tablename__ = "edge_types"

    id: Mapped[int] = Column(BIGINT, Identity(), primary_key=True)
    name: Mapped[str] = Column(String, nullable=True)
    description: Mapped[str] = Column(String, nullable=True)
