<template>
  <div class="m-overlay">
    <misa-popup title="Thông tin chủ đề" width="800px">
      <template #header__close>
        <misa-icon
          @click="$emit('closeAddQuestionForm')"
          icon="close"
          style="margin-left: 3px"
          title="Đóng (ESC)"
        />
      </template>
      <template #content__input-control>
        <misa-textfield
          :errorText="errorQuestion.fullName"
          v-model="addQuestion.fullName"
          type="text"
          idInput="add__full-name"
          labelText="Họ và tên"
          :inputRequired="true"
          class="w1"
          ref="fullName"
        />
        <misa-textfield
          :errorText="errorQuestion.title"
          v-model="addQuestion.title"
          type="text"
          idInput="add__title"
          labelText="Tiêu đề"
          :inputRequired="true"
          class="w1"
          ref="title"
        />
        <misa-radio-input
          :errorText="errorQuestion.subject"
          v-model="addQuestion.subject"
          :values="subjectOptions"
          nameRadioGroup="add__subject"
          :align="'row'"
          type="text"
          labelText="Loại chủ đề"
          class="w1"
        />
        <textarea
          v-model="addQuestion.content"
          placeholder="Nhập nội dụng"
          name="add__content"
          id="add__content"
          cols="30"
          rows="10"
        ></textarea>
      </template>
      <template #footer>
        <misa-separation-line
          style="border-color: var(--border-color-default); margin: 16px 0px"
        />
        <div
          class="flex-row"
          style="justify-content: end; padding-bottom: 16px"
        >
          <misa-button
            type="sub"
            width="56px"
            borderRadius="var(--border-radius-default)"
            style="margin-right: 10px"
            @clickBtnContainer="$emit('closeAddQuestionForm')"
            >Hủy bỏ</misa-button
          >
          <misa-button
            type="main"
            borderRadius="var(--border-radius-default)"
            @clickBtnContainer="storeBtnClick"
            >Lưu</misa-button
          >
        </div>
      </template>
    </misa-popup>

    <div v-if="isShowDialogError" class="m-overlay">
      <misa-popup
        :haveHeader="false"
        width="444px"
        height="auto"
        style="padding: 16px 20px 10px"
      >
        <template #content__input-control>
          <div
            style="
              display: flex;
              align-items: center;
              column-gap: 26px;
              padding-top: 8px;
            "
          >
            <misa-icon height="auto" width="auto" icon="error--medium" />
            <span>{{ getFirstError.errorText }}</span>
          </div>
        </template>

        <template #footer>
          <misa-separation-line
            style="
              border-color: var(--border-color-default);
              margin: 16px 0px 10px;
            "
          />
          <div style="width: 100%; display: flex; justify-content: center">
            <misa-button
              type="main"
              width="58px"
              borderRadius="var(--border-radius-default)"
              padding="0px 12px"
              @click="closeBtnDialogErrorClick"
              >Đóng</misa-button
            >
          </div>
        </template>
      </misa-popup>
    </div>

    <misa-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import questionService from "@/service/QuestionService.js";

export default {
  name: "AddQuestionForm",
  data() {
    return {
      isShowDialogError: false,
      isLoading: false,

      subjectOptions: [
        {
          id: this.$_MISAEnum.SUBJECT.QUESTION,
          name: "Hỏi đáp",
        },
        {
          id: this.$_MISAEnum.SUBJECT.DISCUSS,
          name: "Thảo luận",
        },
        {
          id: this.$_MISAEnum.SUBJECT.SHARE,
          name: "Chia sẻ",
        },
      ],

      addQuestion: {
        fullName: "",
        title: "",
        content: "",
        subject: this.$_MISAEnum.SUBJECT.QUESTION,
      },

      errorQuestion: {
        fullName: "",
        title: "",
        content: "",
        subject: "",
      },
    };
  },
  methods: {
    /**
     * xử lý khi ấn vào nút "Lưu"
     * @author: TTANH (01/07/2023)
     */
    async storeBtnClick() {
      try {
        let isSuccess = await this.createNewQuestion();

        if (isSuccess) {
          this.$emit("closeAddQuestionForm");
          this.$emit("reloadData");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddQuestionPopup.vue:688 ~ storeBtnClick ~ error:",
          error
        );
      }
    },
    /**
     * validate và tạo 1 question mới
     * @author: TTANH (01/07/2023)
     */
    async createNewQuestion() {
      if (this.validateData()) {
        let isSuccess = true;
        this.isLoading = true;

        const res = await questionService.post(this.addQuestion);

        if (res.success) {
          this.$store.commit("addToast", {
            type: "success",
            text: this.$_MISAResource[this.$store.state.langCode].AddQuestion
              .Success,
          });
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: res.devMsg,
          });

          isSuccess = false;
        }

        this.isLoading = false;
        return isSuccess;
      } else {
        this.isShowDialogError = true;
      }
    },
    /**
     * thực hiện validate dữ liệu
     * @author: TTANH (01/07/2023)
     * @returns: true nếu form đã được validate
     */
    validateData() {
      try {
        let isValidate = true;

        this.resetErrorText();

        if (this.addQuestion.fullName === "") {
          this.errorQuestion.fullName =
            this.$_MISAResource[
              this.$store.state.langCode
            ].FullNameInvalidError.Empty;

          isValidate = false;
        }

        if (this.addQuestion.title === "") {
          this.errorQuestion.title =
            this.$_MISAResource[
              this.$store.state.langCode
            ].TitleInvalidError.Empty;

          isValidate = false;
        }

        return isValidate;
      } catch (error) {
        console.log(
          "🚀 ~ file: AddQuestionPopup.vue:509 ~ validateData ~ error:",
          error
        );
      }
    },
    /**
     * làm mới lại thông báo lỗi
     * @author: TTANH (01/07/2023)
     */
    resetErrorText() {
      try {
        for (let attr in this.errorQuestion) {
          this.errorQuestion[attr] = "";
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddQuestionPopup.vue:594 ~ resetErrorText ~ error:",
          error
        );
      }
    },
    /**
     * xử lý khi ấn ẩn dialog thông báo lỗi
     * @author: TTANH (01/07/2023)
     */
    closeBtnDialogErrorClick() {
      try {
        this.isShowDialogError = false;

        this.$refs[this.getFirstError.errorAttr].focus();
      } catch (error) {
        console.log(
          "🚀 ~ file: AddQuestionPopup.vue:777 ~ closeBtnDialogErrorClick ~ error:",
          error
        );
      }
    },
  },
  computed: {
    getFirstError() {
      let errorAttr = "";
      let errorText = "";

      for (let attr in this.errorQuestion) {
        if (this.errorQuestion[attr] !== "") {
          errorText = this.errorQuestion[attr];
          errorAttr = attr;
          break;
        }
      }

      return {
        errorAttr,
        errorText,
      };
    },
  },
};
</script>

<style scoped>
@import url(./add-question-form.css);
</style>
