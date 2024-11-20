export interface RegisterRequest {
  userName: string;
  email: string;
  password: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export const registerUser = async (registerRequest: RegisterRequest) => {
  await fetch("http://localhost:5188/Register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(registerRequest),
  });
};

export const loginUser = async (loginRequest: LoginRequest) => {
  await fetch("http://localhost:5188/Login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(loginRequest),
    credentials: "include",
  });
};
