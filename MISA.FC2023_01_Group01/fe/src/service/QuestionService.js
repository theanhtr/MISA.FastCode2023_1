import BaseService from "./BaseService";

class QuestionService extends BaseService {
  constructor() {
    super("Question");
  }
}

const questionService = new QuestionService();

export default questionService;
