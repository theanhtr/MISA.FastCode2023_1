<template>
  <div class="page">
    <div class="page__header">
      <div class="page__title">Thảo luận/ Hỏi đáp</div>
      <misa-button
        @clickButton="isShowAddQuestionForm = true"
        type="main"
        borderRadius="4px"
        icon="search"
        >Thêm thảo luận/câu hỏi</misa-button
      >
    </div>
    <div class="page__container">
      <div class="container__button">
        <misa-search-input width="300px" v-model="searchText" />
        <misa-button
          type="icon"
          icon="page__reload--black"
          borderRadius="var(--border-radius-default)"
          @click="reloadData"
        />
      </div>
      <div class="container__content">
        <QuestionInfo
          v-for="question in questions"
          :key="question.question_id"
          :questionInfo="question"
        />
      </div>
    </div>
    <AddQuestionForm
      v-if="isShowAddQuestionForm"
      @closeAddQuestionForm="isShowAddQuestionForm = false"
      @reloadData="reloadData"
    />
  </div>
</template>

<script>
import questionService from "@/service/QuestionService.js";
import subjectService from "../../service/SubjectService";
import QuestionInfo from "./child-component/QuestionInfo.vue";
import AddQuestionForm from "./child-component/AddQuestionForm.vue";

export default {
  name: "QAMain",
  components: {
    QuestionInfo,
    AddQuestionForm,
  },
  async created() {
    // this.getQuestions();
    await this.getQuestions();
  },
  data() {
    return {
      searchText: "",
      questions: [],
      isShowAddQuestionForm: false,
    };
  },
  methods: {
    /**
     * thực hiện get dữ liệu câu hỏi khi component được render
     */
    async getQuestions() {
      try {
        const res = await questionService.get();

        if (res.success) {
          this.questions = res.data;
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: this.$_MISAResource[this.$store.state.langCode].QuestionError
              .Get,
          });
        }
      } catch (error) {
        console.log("🚀 ~ file: QAMain.vue:53 ~ getQuestions ~ error:", error);
      }
    },

    /**
     * cập nhật lại employees mới
     * @author: TTANH (03/07/2023)
     */
    async reloadData() {
      try {
        this.questions = [];
        await this.getQuestions();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:465 ~ reloadData ~ error:",
          error
        );
      }
    },
  },
};
</script>

<style>
@import url(./qa-main.css);
</style>
