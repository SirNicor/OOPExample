
<template>
  <el-form :model = "studentTypeOfPage" :rules = "rules" ref = "formRef">
      <el-row type="flex" justify="space-between" >
        <el-col :span="6">
          <el-form-item class = "Fio" inline label = "Имя: " label-position="top" prop = "firstName">
            <el-input v-model = "studentTypeOfPage.firstName" ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item class = "Fio" inline label = "Фамилия: " label-position="top" prop = "lastName">
            <el-input v-model = "studentTypeOfPage.lastName"></el-input>
          </el-form-item> 
        </el-col>
        <el-col :span="6">
          <el-form-item class = "Fio" inline label = "Отчество: " label-position="top" prop = "middleName">
            <el-input v-model = "studentTypeOfPage.middleName"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item class = "Fio" inline label = "Дата рождения: " label-position="top" prop = "dob">
            <el-date-picker v-model = "studentTypeOfPage.dob"
                            format="DD.MM.YYYY"></el-date-picker>
          </el-form-item>
        </el-col>
      </el-row>
    <el-form-item class = "Address" inline label = "Адрес" label-position="top">
      <el-row type="flex" justify="space-between">
        <el-col :span="6">
          <p>Страна: </p>
          <el-input v-model = "studentTypeOfPage.country"></el-input>
        </el-col>
        <el-col :span="6">
          <p>Город: </p>
          <el-input v-model = "studentTypeOfPage.city"></el-input>
        </el-col>
        <el-col :span="6">
          <p>Улица: </p>
          <el-input v-model = "studentTypeOfPage.state"></el-input>
        </el-col>
        <el-col :span="6">
          <p>Номер дома: </p>
          <el-input v-model = "studentTypeOfPage.houseNumber"></el-input>
        </el-col>
      </el-row>
    </el-form-item>
      <el-row type="flex" justify="space-between">
        <el-col :span="8">
          <el-form-item class = "Fio" inline label = "Серия: " label-position="top" prop = "serial">
            <el-input v-model = "studentTypeOfPage.serial"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item class = "Fio" inline label = "Номер: " label-position="top" prop = "number">
            <el-input v-model = "studentTypeOfPage.number"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item class = "Fio" inline label = "Кем выдан: " label-position="top" prop = "placeReceipt">
            <el-input v-model = "studentTypeOfPage.placeReceipt"></el-input>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row type="flex" justify="space-between" label = "Общая инфо:" label-position="top">
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
    <el-row type="flex" justify="space-between">
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
    <el-form-item inline>
      <el-row type = "flex" justify = "space-between">
        <el-button type = "primary" @click = "Send">Отправить изменения</el-button>
        <el-button type = "primary" plain @click="DeleteDialogBool = true">Удалить</el-button>
        <el-button type = "primary" @click = "Reset">Очистить изменения</el-button>
        <el-button type = "primary" @click = "GoRegistry">Вернуться к реестру</el-button>
        <DeleteDialogComponent @confirm = "Delete" v-model = "DeleteDialogBool"></DeleteDialogComponent>
      </el-row>
    </el-form-item>
  </el-form>
</template>

<style scoped>
  .Fio
  {
    width: 100%;
  }
</style>

<script setup lang="ts">
  import {computed, onMounted, reactive, ref, watch} from 'vue';
  import {ElMessageBox, type FormInstance, type FormRules} from "element-plus";
  import api from "@/Api.ts";
  import type {StudentsType, StudentsTypeForPage} from "@/types/studentType.ts";
  import {useRoute} from "vue-router";
  import DeleteDialogComponent from "@/components/DeleteDialogComponent.vue";
  import router from '@/router/index.ts';

  const viewportWidth = window.innerWidth;
  const viewportHeight = window.innerHeight;
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
        country: "",
        city: "",
        state: "",
        houseNumber: "",
        serial: "",
        number: "",
        placeReceipt: ""
      });
  const route = useRoute();
  async function LoadData()
  {
    debugger;
    let response = await api.get(`Student/${route.params.index}`);
    studentTypeOfPage.value = response.data;
  }
  onMounted(async () =>
  {
    await LoadData();
  })
  async function Reset(){
    await LoadData();
  }
  function GoRegistry()
  {
    window.history.back();
  }
  async function Delete(){
    await api.delete(`Student/${route.params.index}`);
    window.history.back();
  }
  const DeleteDialogBool = ref(false);
  async function Send()
  {
    let student = studentTypeOfPage.value;
    debugger;
    let param = route.params;
    if(route.params.index === 'undefined')
    {
      let id = await api.post(`Student`, student) as number;
      router.replace(`Student/${id}`);
    }
    else
    {
      await api.put(`Student/${route.params.index}`, student)
      await LoadData();
    }
  }
  const rules : FormRules<typeof studentTypeOfPage> = {
    firstName: [{max: 30, message: "Слишком длинное имя", trigger: ['blur', 'change']},
      {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']}],
    lastName: [{max: 30, message: "Слишком длинная фамилия", trigger: ['blur', 'change']},
      {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']}],
    middleName: {max: 30, message: "Слишком длинное отчество", trigger: ['blur', 'change']},
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
    course: {required: true, message: "Не может быть пустым", trigger: ['blur', 'change']},
    countOfExamsPassed: {},
  }
</script> 
