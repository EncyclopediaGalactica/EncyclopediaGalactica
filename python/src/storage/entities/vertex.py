from sqlalchemy import Column, String, BIGINT, Identity
from sqlalchemy.orm import Mapped

from core.db import EGBase


class Vertex(EGBase):
    __tablename__ = "vertex"
    id: Mapped[int] = Column(BIGINT,Identity(), primary_key=True)
    data: Mapped[str] = Column(String, nullable=True)