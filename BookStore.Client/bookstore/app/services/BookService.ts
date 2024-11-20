export interface BookRequest {
  title: string;
  description: string;
  price: number;
}

export const getAllBooks = async () => {
  const response = await fetch("http://localhost:5188/api/Books", {
    method: "GET",
    credentials: "include",
  });

  return response.json();
};

export const createBook = async (bookRequest: BookRequest) => {
  await fetch("http://localhost:5188/api/Books", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
    body: JSON.stringify(bookRequest),
  });
};

export const updateBook = async (id: string, bookRequest: BookRequest) => {
  await fetch(`http://localhost:5188/api/Books/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
    body: JSON.stringify(bookRequest),
  });
};

export const deleteBook = async (id: string) => {
  await fetch(`http://localhost:5188/api/Books/${id}`, {
    method: "DELETE",
    credentials: "include",
  });
};
