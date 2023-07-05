const MISAResource = {
  VN: {
    CustomerCodeInvalidError: {
      NewCodeError: "Lấy mã mới thất bại, vui lòng liên hệ với MISA.",
      Empty: "Mã không được phép để trống.",
    },
    FullNameInvalidError: {
      Empty: "Tên không được phép để trống.",
    },
    TitleInvalidError: {
      Empty: "Tiêu đề không được phép để trống.",
    },
    ContentInvalidError: {
      Empty: "Tiêu đề không được phép để trống.",
    },
    DepartmentInvalidError: {
      Empty: "Đơn vị không được phép để trống.",
      NotFound: "Dữ liệu <Đơn vị> không có trong danh mục.",
    },
    EmailInvalidError: {
      Format: "Email không đúng định dạng.",
    },

    ToastTitle: {
      Info: "Thông tin!",
      Warning: "Cảnh báo!",
      Error: "Lỗi!",
      Success: "Thành công!",
    },

    Paging: {
      Title: " bản ghi trên 1 trang",
      Error: " lỗi phân trang",
    },

    DepartmentError: {
      Get: "Lấy thông tin phòng ban bị lỗi.",
    },

    QuestionError: {
      Get: "Lấy thông tin câu hỏi bị lỗi.",
    },
    AddQuestion: {
      Success: "Câu hỏi đã được thêm.",
      Error: "Gặp lỗi khi thêm Câu hỏi.",
      SameCodeError: "đã tồn tại trong hệ thống, vui lòng kiểm tra lại.",
    },
    UpdateEmployee: {
      Success: "Thông tin nhân viên đã được cập nhật.",
      Error: "Gặp lỗi khi cập nhật thông tin nhân viên.",
    },
    DeleteEmployee: {
      Success: "Xóa thành công nhân viên ",
      NotFound: "Không tìm thấy nhân viên để xóa.",
      Error: "Gặp lỗi khi xóa nhân viên.",
    },
  },

  EN: {
    CustomerCodeInvalidError: {
      NewCodeError: "Get new code error, please contact MISA.",
      Empty: "Code is not empty.",
    },
    FullNameInvalidError: {
      Empty: "Name is not empty.",
    },
    TitleInvalidError: {
      Empty: "Title is not empty.",
    },
    DepartmentInvalidError: {
      Empty: "Department is not empty.",
      NotFound: "Department not found.",
    },
    EmailInvalidError: {
      Format: "Email wrong format.",
    },

    ToastTitle: {
      Info: "Info!",
      Warning: "Warning!",
      Error: "Error!",
      Success: "Success!",
    },

    Paging: {
      Title: " record per page",
      Error: " error paging",
    },

    DepartmentError: {
      Get: "Get the department information is error.",
    },

    QuestionError: {
      Get: "Get the question information is error.",
    },
    AddQuestion: {
      Success: "question has been added.",
      Error: "Error while adding question.",
      SameCodeError: "already exists in the system, please check again.",
    },
    UpdateEmployee: {
      Success: "Employee information has been updated.",
      Error: "Error while updating employee information.",
    },
    DeleteEmployee: {
      Success: "Successfully deleted employee ",
      NotFound: "Couldn't find an employee to delete.",
      Error: "Got an error when deleting an employee.",
    },
  },
};

export default MISAResource;
