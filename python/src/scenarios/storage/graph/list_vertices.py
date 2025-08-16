from core.db import get_session
from storage.api.vertex import VertexApi


def list_vertices()-> None:
    with next(get_session()) as session:
        vertex_api = VertexApi(session)
        result = vertex_api.list_vertices()
        for v in result:
            print(v)
    session.commit()

