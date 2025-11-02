from sqlalchemy import Column, String, BIGINT, Identity
from sqlalchemy.orm import Mapped
from storage.db import EGBase


class Edge(EGBase):
    __tablename__ = "edges"
    id: Mapped[int] = Column(BIGINT, Identity(), primary_key=True)
    edge_type_id: Mapped[int] = Column(BIGINT)
    from_vertex_id: Mapped[int] = Column(BIGINT)
    to_vertex_id: Mapped[int] = Column(BIGINT)
