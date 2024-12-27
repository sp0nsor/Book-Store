import { Input, Modal } from "antd";
import { RegisterRequest, LoginRequest } from "../services/AuthService";
import { useState } from "react";

interface Props {
  mode: Mode;
  isModalOpen: boolean;
  handleCancel: () => void;
  handleRegister: (request: RegisterRequest) => void;
  handleLogin: (request: LoginRequest) => void;
}

export enum Mode {
  Login,
  Register,
}

export default function RegisterForm({
  mode,
  isModalOpen,
  handleCancel,
  handleRegister,
  handleLogin,
}: Props) {
  const [userName, setUserName] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const handleOnOk = async () => {
    if (mode === Mode.Login) {
      const loginRequest = { email, password };
      handleLogin(loginRequest);
    } else {
      const registerRequest = { userName, email, password };
      handleRegister(registerRequest);
    }
  };

  return (
    <Modal
      title={mode === Mode.Register ? "Регистрация" : "Вход"}
      open={isModalOpen}
      onCancel={handleCancel}
      onOk={handleOnOk}
      cancelText={"Отмена"}
    >
      <div className="book_modal">
        {mode == Mode.Register && (
          <Input
            placeholder="Имя пользователя"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />
        )}
        <Input
          placeholder="Почта"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <Input
          placeholder="Пароль"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>
    </Modal>
  );
}
