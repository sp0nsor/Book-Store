import { Input, Modal } from "antd";
import { BuyBookRequest } from "../services/BookService";
import { useState } from "react";

interface Props {
  bookPrice: number;
  isModalOpen: boolean;
  handleCancel: () => void;
  handleBuyBook: (request: BuyBookRequest) => void;
}

export default function BuyBookForm({
  bookPrice,
  isModalOpen,
  handleCancel,
  handleBuyBook,
}: Props) {
  const [accountNumber, setAccountNumber] = useState<string>("");
  const [key, setKey] = useState<string>("");

  const handleOnOk = async () => {
    const purchaseRequest: BuyBookRequest = {
      senderAccountNumber: accountNumber,
      senderSecretKey: key,
      bookPrice,
    };
    handleBuyBook(purchaseRequest);
  };

  return (
    <Modal
      title="Окно для покупки"
      open={isModalOpen}
      onCancel={handleCancel}
      onOk={handleOnOk}
      cancelText={"Отмена"}
    >
      <div className="book_modal">
        <Input
          placeholder="Счет"
          value={accountNumber}
          onChange={(e) => setAccountNumber(e.target.value)}
        />
        <Input
          placeholder="Ключ"
          value={key}
          onChange={(e) => setKey(e.target.value)}
        />
      </div>
    </Modal>
  );
}
