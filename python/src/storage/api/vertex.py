from sqlalchemy.orm import Session

from storage.entities.vertex import Vertex


class VertexApi:

    def __init__(self, session: Session):
        if session is None:
            raise Exception("Session is None")
        super().__init__()
        self.session = session

    def list_vertices(self):
            result = self.session.query(Vertex).all()
            for row in result:
                print(f"{row.id} {row.data}")
