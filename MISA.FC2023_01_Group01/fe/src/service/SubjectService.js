import BaseService from "./BaseService";

class SubjectService extends BaseService {
  constructor() {
    super("Subject");
  }
}

const subjectService = new SubjectService();

export default subjectService;
