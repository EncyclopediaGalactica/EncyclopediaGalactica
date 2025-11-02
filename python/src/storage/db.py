import os
from abc import ABC, abstractmethod
from typing import Any, Generator

from dotenv import load_dotenv
from sqlalchemy.exc import SQLAlchemyError
from sqlalchemy.orm import DeclarativeBase, Session, sessionmaker
from sqlalchemy import create_engine


class DatabaseConfig(ABC):
    @abstractmethod
    def get_connection(self) -> str:
        pass


class ProdDatabaseConfig(DatabaseConfig):
    def get_connection(self) -> str:
        load_dotenv(dotenv_path=".env")
        db_host = os.getenv("DB_HOST")
        db_port = os.getenv("DB_PORT")
        db_name = os.getenv("DB_NAME")
        db_user = os.getenv("DB_USER")
        db_password = os.getenv("DB_PASSWORD")
        return f"postgresql://{db_user}:{db_password}@{db_host}:{db_port}/{db_name}"


config = ProdDatabaseConfig()
ENGINE = create_engine(config.get_connection())  # noqa: F821

SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=ENGINE)


class EGBase(DeclarativeBase):  # noqa: F821
    pass


# drop and recreate tables
# metadata = MetaData()
# EGBase.metadata.drop_all(ENGINE)
# EGBase.metadata.create_all(ENGINE)


def get_session() -> Generator[Session, Any, None]:
    session = SessionLocal()
    try:
        yield session
    except SQLAlchemyError as e:
        session.rollback()
        raise e
    finally:
        session.close()
