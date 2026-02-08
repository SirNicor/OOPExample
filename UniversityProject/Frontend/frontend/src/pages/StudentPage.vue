<template>
  <TableComponent :columns="Columns" :data="Data" :apiBase = "Url"/>
</template>

<style scoped>
</style>

<script setup lang="ts">
  import {onMounted, reactive, ref, watch} from 'vue'
  import {useRoute} from 'vue-router'
  import TableComponent from '@/components/TableComponent.vue'
  import type {TableColumn, TableData} from '@/types/TableTypes';
  import axios, {type AxiosResponse, type AxiosError} from "axios"
  import api from "@/Api.ts"
  import {type StudentType, type StudentServer, mapStudentType} from '@/types/studentType.ts'
  import router from '@/router/index.ts'
  const Url = window.location.href;
  const students = ref<StudentType[]>([]);
  const Data = ref<TableData[]>([]);
  const Columns = reactive<TableColumn[]>( [
    {key: "firstName-col", dataKey: 'firstName', title: 'Имя'},
    {key: "lastName-col", dataKey: 'lastName', title: 'Фамилия'},
    {key: "middleName-col", dataKey: 'middleName', title: 'Отчество'},
    {key: "birthData-col", dataKey: 'birthData', title: 'Дата рождения'},
    {key: "serial-col", dataKey: 'serial', title: 'Серия'},
    {key: "number-col", dataKey: 'number', title: 'Номер'},
    {key: "totalScore-col", dataKey: 'totalScore', title: 'Средний балл'},
    {key: "skipHours-col", dataKey: 'skipHours', title: 'Часы пропусков'},
    {key: "countOfExamsPassed-col", dataKey: 'countOfExamsPassed', title: 'Сданные экзамены'},
    {key: "course-col", dataKey: 'course', title: 'Курс'},
    {key: "country-col", dataKey: 'country', title: 'Страна'},
    {key: "city-col", dataKey: 'city', title: 'Город'},
    {key: "street-col", dataKey: 'street', title: 'Улица'},
    {key: "houseNumber-col", dataKey: 'houseNumber', title: 'Номер дома'},
  ]);
  const route = useRoute();
  async function loadData()
  {
    Data.value = [];
    students.value = [];
    let response;
    if(route.query.sortKey === undefined || route.query.sortKey === null) {
      response = await api.get('/student');
    }
    else
    {
      response = await api.get('student/${route.query.sortKey}/${route.query.sortType}');
    }
    let studentServers = response.data as StudentServer[];
    students.value = studentServers.map(mapStudentType);
    students.value.forEach((student) => {
      student.birthData = Intl.DateTimeFormat("ru-RU").format(new Date(student.birthData));
    })
    for(let i = 0; i < students.value.length; i++) {
      Data.value.push({id: i.toString(), ...students.value[i]});
    }
  }
  watch(() => route.query,
  async(newQuery, oldQuery) => {
    await loadData();
  },
      {deep: true})
  onMounted(async () => {
    try {
      debugger;
      await loadData();
    }
    catch (error) {
      console.error(error);
    }
  })
</script>