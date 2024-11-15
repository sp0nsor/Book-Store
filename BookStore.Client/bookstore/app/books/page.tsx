"use client";

import { Button, Typography } from "antd";
import { Books } from "../components/Books";
import { useEffect, useState } from "react";
import {
  BookRequest,
  createBook,
  deleteBook,
  getAllBooks,
  updateBook,
} from "../services/book";
import { CreateUpdateBook, Mode } from "../components/CreateUpdateBook";

const { Title } = Typography;

export default function BooksPage() {
  const defaultValues = {
    title: "",
    description: "",
    price: 1,
  } as Book;
  const [values, setValues] = useState<Book>(defaultValues);

  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [mode, setMode] = useState(Mode.Create);

  useEffect(() => {
    const getBooks = async () => {
      const books = await getAllBooks();
      setLoading(false);
      setBooks(books);
    };

    getBooks();
  }, []);

  const handleCreateBook = async (request: BookRequest) => {
    await createBook(request);
    closeModal();

    const books = await getAllBooks();
    setBooks(books);
  };

  const handleupdateBook = async (id: string, request: BookRequest) => {
    await updateBook(id, request);
    closeModal();

    const books = await getAllBooks();
    setBooks(books);
  };

  const handleDeleteBook = async (id: string) => {
    await deleteBook(id);

    const books = await getAllBooks();
    setBooks(books);
  };

  const openEditModal = (book: Book) => {
    setMode(Mode.Edit);
    setValues(book);
    setIsModalOpen(true);
  };

  const opentModal = () => {
    setMode(Mode.Create);
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setValues(defaultValues);
    setIsModalOpen(false);
  };

  return (
    <div>
      <Button onClick={opentModal}>Добавить книгу</Button>
      <CreateUpdateBook
        mode={mode}
        values={values}
        isModalOpen={isModalOpen}
        handleCreate={handleCreateBook}
        handleUpdate={handleupdateBook}
        handleCancel={closeModal}
      />
      {loading ? (
        <Title>Loading...</Title>
      ) : (
        <Books
          books={books}
          handleOpen={openEditModal}
          handleDelete={handleDeleteBook}
        />
      )}
    </div>
  );
}
