"use client";

import dynamic from "next/dynamic";
import { LoginRequest, RegisterRequest } from "../services/AuthService";
import { useState } from "react";
import { registerUser, loginUser } from "../services/AuthService";
import { Button } from "antd";
import { Mode } from "../components/RegisterForm";
const RegisterForm = dynamic(() => import("../components/RegisterForm"), {
  ssr: false,
});

export default function RegisterPage() {
  const defaultValues = {
    userName: "",
    email: "",
    password: "",
  } as RegisterRequest;

  const [values, setValues] = useState<RegisterRequest>(defaultValues);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [mode, setMode] = useState(Mode.Register);

  const handleRegister = async (request: RegisterRequest) => {
    await registerUser(request);
    closeModal();
  };

  const handleLogin = async (request: LoginRequest) => {
    console.log(request);
    await loginUser(request);
    closeModal();
  };

  const openModal = () => {
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setValues(defaultValues);
    setIsModalOpen(false);
  };

  return (
    <div>
      <Button
        onClick={() => {
          setMode(Mode.Register);
          openModal();
        }}
      >
        Регистрация
      </Button>
      <Button
        onClick={() => {
          setMode(Mode.Login);
          openModal();
        }}
      >
        Вход
      </Button>
      <RegisterForm
        mode={mode}
        isModalOpen={isModalOpen}
        handleRegister={handleRegister}
        handleCancel={closeModal}
        handleLogin={handleLogin}
      />
    </div>
  );
}
