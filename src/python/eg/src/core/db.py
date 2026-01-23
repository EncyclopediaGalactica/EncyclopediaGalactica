from abc import ABC, abstractmethod
from typing import Any, Generator

from sqlalchemy import create_engine, MetaData
from sqlalchemy.orm import sessionmaker, Session, DeclarativeBase
from sqlalchemy.exc import SQLAlchemyError

class DatabaseConfig(ABC):
    @abstractmethod
    def get_connection(self) -> str:
        pass

class ProdDatabaseConfig(DatabaseConfig):
    def get_connection(self) -> str:
        return "postgresql://eg:eguser@localhost:5432/eg"

config = ProdDatabaseConfig()
ENGINE = create_engine(config.get_connection())  # noqa: F821

SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=ENGINE)

class EGBase(DeclarativeBase):  # noqa: F821
    pass

# drop and recreate tables
from storage.entities import vertex

metadata = MetaData()
EGBase.metadata.drop_all(ENGINE)
EGBase.metadata.create_all(ENGINE)

def get_session() -> Generator[Session, Any, None]:
    session = SessionLocal()
    try:
        yield session
    except SQLAlchemyError as e:
        session.rollback()
        raise e
    finally:
            session.close()

