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
    {key: "id-col", dataKey: 'id', title: 'ID'},
    {key: "personId-col", dataKey: 'personId', title: 'Person Id'},
    {key: "totalScore-col", dataKey: 'totalScore', title: 'Total Score'},
    {key: "skipHours-col", dataKey: 'skipHours', title: 'Skip Hours'},
    {key: "creditScores-col", dataKey: 'creditScores', title: 'Credit Scores'},
    {key: "countOfExamsPassed-col", dataKey: 'countOfExamsPassed', title: 'Count Of Exams Passed'},
    {key: "course-col", dataKey: 'course', title: 'Course'},
    {key: "passportId-col", dataKey: 'passportId', title: 'Passport Id'},
    {key: "serial-col", dataKey: 'serial', title: 'Serial'},
    {key: "number-col", dataKey: 'number', title: 'Number'},
    {key: "firstName-col", dataKey: 'firstName', title: 'First Name'},
    {key: "lastName-col", dataKey: 'lastName', title: 'LastName'},
    {key: "middleName-col", dataKey: 'middleName', title: 'Middle Name'},
    {key: "birthData-col", dataKey: 'birthData', title: 'Birth Data'},
    {key: "addressId-col", dataKey: 'addressId', title: 'Address Id'},
    {key: "country-col", dataKey: 'country', title: 'Country'},
    {key: "city-col", dataKey: 'city', title: 'City'},
    {key: "street-col", dataKey: 'street', title: 'Street'},
    {key: "houseNumber-col", dataKey: 'houseNumber', title: 'House Number'},
  ]);
  const route = useRoute();
  async function loadData()
  {
    Data.value = [];
    students.value = [];
    let response;
    if(route.query.sortKey === undefined || route.query.sortKey === null) {
      response = await axios.get('https://localhost:7082/student');
    }
    else
    {
      response = await axios.get(`https://localhost:7082/student/${route.query.sortKey}/${route.query.sortType}`,);
    }
    let studentServers = response.data as StudentServer[];
    students.value = studentServers.map(mapStudentType);
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