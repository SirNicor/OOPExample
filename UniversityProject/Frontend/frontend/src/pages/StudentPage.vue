
<template>
  <h1>{{headerText}}</h1>
  <el-form :model = "studentTypeOfPage" :rules = "rules" ref = "formRef">
      <el-row type="flex" justify="space-between" class = "rowStudent">
        <el-col :span="5">
          <el-form-item class = "Fio" inline label = "Имя: " label-position="top" prop = "firstName">
            <el-input v-model = "studentTypeOfPage.firstName" ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="5">
          <el-form-item class = "Fio" inline label = "Фамилия: " label-position="top" prop = "lastName">
            <el-input v-model = "studentTypeOfPage.lastName"></el-input>
          </el-form-item> 
        </el-col>
        <el-col :span="5">
          <el-form-item class = "Fio" inline label = "Отчество: " label-position="top" prop = "middleName">
            <el-input v-model = "studentTypeOfPage.middleName"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="5">
          <el-form-item class = "Fio" inline label = "Дата рождения: " label-position="top" prop = "dob">
            <el-date-picker v-model = "studentTypeOfPage.dob"
                            format="DD.MM.YYYY"></el-date-picker>
          </el-form-item>
        </el-col>
      </el-row>
        <el-form-item inline label = "Адрес: " label-position="top" prop = "address" class = "rowStudent">
          <el-autocomplete :fetch-suggestions = "addressSearch" @select = "handleSelect" v-model = "studentTypeOfPage.address"></el-autocomplete>
        </el-form-item>
      <el-row type="flex" justify="space-between" class = "rowStudent">
        <el-col :span="7">
          <el-form-item class = "Fio" inline label = "Серия: " label-position="top" prop = "serial">
            <el-input v-model = "studentTypeOfPage.serial"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="7">
          <el-form-item class = "Fio" inline label = "Номер: " label-position="top" prop = "number">
            <el-input v-model = "studentTypeOfPage.number"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="7">
          <el-form-item class = "Fio" inline label = "Кем выдан: " label-position="top" prop = "placeReceipt">
            <el-input v-model = "studentTypeOfPage.placeReceipt"></el-input>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row type="flex" justify="space-between" label = "Общая инфо:" label-position="top" class = "rowStudent">
          <el-col :span="6">
          <el-form-item inline label = "ID телеграмм чата: " label-position="top" prop = "chatId">
            <el-input v-model = "studentTypeOfPage.chatId"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="4">
          <el-form-item inline label = "Судимость(есть/нет): " label-position="top" prop = "criminalRecord">
            <el-switch v-model = "studentTypeOfPage.criminalRecord" active-text="Да" inactive-text="Нет"/>
          </el-form-item>
        </el-col>
        <el-col :span="3">
          <el-form-item inline label = "Средний балл: " label-position="top" prop = "totalScore">
            <el-input v-model = "studentTypeOfPage.totalScore"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="4">
          <el-form-item inline label = "Часы пропусков: " label-position="top" prop = "skipHours">
              <el-input-number v-model = "studentTypeOfPage.skipHours"></el-input-number>
          </el-form-item>
        </el-col>
      </el-row>
    <el-row type="flex" justify="space-between" class = "rowStudent">
        <el-col :span = "8">
          <el-form-item inline label = "Общие баллы: " label-position="top" prop = "creditScores">
            <el-input-number v-model = "studentTypeOfPage.creditScores"></el-input-number>
          </el-form-item>
        </el-col>
        <el-col :span = "8">
          <el-form-item inline label = "Курс: " label-position="top" prop = "course">
            <el-input-number v-model = "studentTypeOfPage.course"></el-input-number>
          </el-form-item>
        </el-col>
        <el-col :span = "8">
          <el-form-item inline label = "Количество сданных экзаменов: " label-position="top" prop = "countOfExamsPassed">
            <el-input-number v-model = "studentTypeOfPage.countOfExamsPassed"></el-input-number>
          </el-form-item>
        </el-col>
    </el-row>
    <el-form-item inline class = "rowStudent">
      <el-row type = "flex" justify = "space-between">
        <el-button type = "primary" @click = "sendBool = true">Отправить изменения</el-button>
        <el-button type = "danger"  @click="DeleteDialogBool = true" v-if = "deleteButton">Удалить</el-button>
        <el-button type = "text" @click = "Reset">Очистить изменения</el-button>
        <el-button type = "text" @click = "GoRegistry">Назад</el-button>
        <DialogComponent @confirm = "Delete" v-model = "DeleteDialogBool" :question-string = "question"></DialogComponent>
        <DialogComponent @confirm = "SendAndBack" @cancel = "SendNotBack" :question-string = "sendDialog" v-model = "sendBool"></DialogComponent>
      </el-row>
    </el-form-item>
  </el-form>
</template>

<style scoped>
  .Fio
  {
    width: 100%;
  }
  .rowStudent
  {
    margin-bottom: 1.5%;
    margin-left: 5px;
    margin-right: 5px;
  }
</style>

<script setup lang="ts">
  import {computed, onMounted, ref, watch} from 'vue';
  import {ElMessageBox, type FormInstance, type FormRules} from "element-plus";
  import api from "@/api/Api.ts";
  import {StudentResponse} from "@/api/Student.ts";
  import {AddressResponse} from "@/api/Address.ts";
  import type {StudentsTypeForPage} from "@/types/studentType.ts";
  import {useRoute} from "vue-router";
  import DialogComponent from "@/components/DialogComponent.vue";
  import router from '@/router/index.ts';
  
  const formRef = ref<FormInstance>()
  const studentTypeOfPage = ref<StudentsTypeForPage>(
      {
        studentId: 0,
        passportId: 0,
        addressId: 0,
        criminalRecord: false,
        totalScore: 0,
        skipHours: 0,
        creditScores: 0, 
        countOfExamsPassed: 0,
        course: 0,
        chatId: 0,
        firstName: "",
        lastName: "",
        middleName: "",
        dob: new Date(),
        address: "",
        country: "",
        state: "",
        city: "",
        houseNumber: "",
        serial: "",
        number: "",
        placeReceipt: ""
      });
  const route = useRoute();
  const suggestions = ref<any>();
  const question = ref("");
  const deleteButton = ref(false);
  const sendDialog = ref("Вернуться ли к реестру после добавления?")
  const sendBool = ref(false);
  const headerText = ref("");
  const handleSelect = (item: Record<string, any>) => {
    studentTypeOfPage.value.address = item.label
    studentTypeOfPage.value.city = item.city
    studentTypeOfPage.value.houseNumber = item.houseNumber
    studentTypeOfPage.value.state = item.state
    studentTypeOfPage.value.country = item.country
  }
  const DeleteDialogBool = ref(false);
  onMounted(async () =>
  {
    if(!(route.params.index === 'undefined'))
    {
      deleteButton.value = true;
      await LoadData();
      headerText.value = "Студент " + studentTypeOfPage.value.lastName + " " + studentTypeOfPage.value.firstName + " " + studentTypeOfPage.value.middleName;
    }
    else
    {
      headerText.value = "Добавление нового студента";
    }
  })
  async function LoadData()
  {
    let response = await StudentResponse.getStudent(route.params.index);
    studentTypeOfPage.value = response.data;
    question.value = "Вы уверены, что хотите удалить студента " + studentTypeOfPage.value.lastName + " " + studentTypeOfPage.value.firstName + "?";
  }
  async function Reset(){
    await LoadData(); 
  }
  function GoRegistry()
  {
    window.history.back();
  }
  async function Delete(){
    await StudentResponse.deleteStudent(route.params.index);
    window.history.back();
  }
  async function SendNotBack()
  {
    let student = studentTypeOfPage.value;
    debugger;
    let param = route.params;
    if(route.params.index === 'undefined')
    {
      let id = await StudentResponse.postStudent(student);
      router.replace(`${id.data}`);
      deleteButton.value = true;
    }
    else
    {
      await StudentResponse.putStudent(route.params.index, student);
      await LoadData(); 
    }
  }
  async function SendAndBack()
  {
    SendNotBack()
    window.history.back();
  }
  async function loadAll(query : string) {
    try {
      const response = await AddressResponse.getSuggest(query);
      debugger;
      return response.data.map((item: any) => ({
        value: item.value,
        label: item.value,
        country: item.data.country,
        city: item.data.city,
        houseNumber: item.data.house,
        state: item.data.street
      }))
    } catch (error) {
      return []
    }
  }
  const addressSearch = async (queryString: string, cb: (results: any[]) => void) => {
    if (!queryString) {
      cb([])
      return
    }

    try {
      const results = await loadAll(queryString)
      cb(results)
    } catch (error) {
      console.error('Ошибка поиска:', error)
      cb([])
    }
  }
  const rules : FormRules<typeof studentTypeOfPage> = {
    firstName: [{max: 30, message: "Слишком длинное имя", trigger: ['blur', 'change']},
      {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']}],
    lastName: [{max: 30, message: "Слишком длинная фамилия", trigger: ['blur', 'change']},
      {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']}],
    middleName: {max: 30, message: "Слишком длинное отчество", trigger: ['blur', 'change']},
    houseNumber: {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']},
    dob: {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']},
    serial: [
      {validator: (rule, value, callback) =>
        {
          if(!/[0-9]/.test(value))
          {
            callback(new Error("Только цифры"));
          }
          if(!(value.length == 4))
          {
            callback(new Error("4 символа"));
          }
          callback();
          return;
        }, trigger: ['blur', 'change']},
      {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']}],
    number: [{
      validator: (rule, value, callback) =>
      {
        if(!/[0-9]/.test(value))
        {
          callback(new Error("Только цифры"));
        }
        if(!(value.length == 6))
        {
          callback(new Error("6 символов"));
        }
        callback();
        return;
      }, trigger: ['blur', 'change']
    }, {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']}],
    placeReceipt: [{required: true, message: "Не может быть пустым", trigger: ['blur', 'change']}],
    skipHours: {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']},
    creditScores: {},
    course: [{required: true, message: "Не может быть пустым", trigger: ['blur', 'change']},
      {validator: (rule, value, callback) =>
        {
          if(value < 1)
          {
            callback(new Error("Минимум - 1 курс"));
          }
          if(value > 6)
          {
            callback(new Error("Максимум - 6 курс"))
          }
          callback();
          return;
        }, trigger: ['blur', 'change']}],
    countOfExamsPassed: {},
    address: [{required: true, message: "Не может быть пустым", trigger: ['blur', 'change']}
    ,{validator: (rule, value, callback) =>
        {
          if(studentTypeOfPage.value.country === undefined)
          {
            callback(new Error("Обязательно указать страну"));
          }
          if(studentTypeOfPage.value.city === undefined)
          { 
            callback(new Error("Обязательно указать город"));
          }
          if(studentTypeOfPage.value.houseNumber === undefined)
          {
            callback(new Error("Обязательно указать номер дома"));
          }
          if(studentTypeOfPage.value.state === undefined)
          {
            callback(new Error("Обязательно указать улицу"));
          }
          callback();
          return;
        }, trigger: ['blur', 'change']}],
  }
</script> 
