<template>
  <el-form inline class = "FilterForm"> 
    <el-form-item>
      <el-date-picker v-model="Filter.FilterBirthDayStart" placeholder= "Возраст от: " type = "date"/>
      <el-date-picker v-model="Filter.FilterBirthDayEnd" placeholder="Возраст до: " type = "date"/>
      <el-button @click = "clearData">clear</el-button>
    </el-form-item>
    <el-form-item label = "Кол-во часов пропусков: ">
      <el-input-number v-model="Filter.FilterSkipHoursStart" :min="1" :max="120" placeholder="От:"/>
      <el-input-number v-model="Filter.FilterSkipHoursEnd" :min="1" :max="120" placeholder="До:" />
      <el-button @click = "clearSkipHours">clear</el-button>
    </el-form-item>
    <el-form-item inline>
      <el-cascader :options = "optionsCourse" placeholder="Курс:"  @change = "SetFilterCourse"/>
    </el-form-item>
    <el-form-item inline>
      <el-input v-model="Filter.FilterTotalScore" placeholder="Средний балл от:"/>
    </el-form-item>
    <el-form-item>
      <el-button type = "primary" @click = "Reset">Фильтровать</el-button>
    </el-form-item>
  </el-form>
  <TableComponent :columns="Columns" :data="Data" :apiBase = "Url" :countPage = "pageCount"/>
</template>

<style scoped>
  .FilterForm {
    margin-top: 1%;
    margin-left:4px;
    padding-left: 4px;
    border: 1px solid;
    padding-top: 1%;
    margin-right: 20%; 
  }
</style>

<script setup lang="ts">
  import {computed, onMounted, reactive, ref, watch} from 'vue'
  import {useRoute} from 'vue-router'
  import TableComponent from '@/components/TableComponent.vue'
  import type {TableColumn, TableData} from '@/types/TableTypes';
  import api from "@/Api.ts"
  import {type StudentsType, type Filter} from '@/types/studentType.ts'
  import router from '@/router/index.ts'
  import {type FormRules, ElLink } from "element-plus";
  const Url = "/student";
  const students = ref<StudentsType[]>([]);
  const Data = ref<TableData[]>([]);
  const pageCount = ref(0);
  const count = 15;
  const Columns = reactive<TableColumn[]>( [
    {dataKey: 'fio', title: 'ФИО'},
    {dataKey: 'dob', title: 'Дата рождения'},
    {dataKey: 'serial', title: 'Серия'},
    {dataKey: 'number', title: 'Номер'},
    {dataKey: 'address', title: 'Адрес'},
    {dataKey: 'totalScore', title: 'Средний балл'},
    {dataKey: 'skipHours', title: 'Часы пропусков'},
    {dataKey: 'course', title: 'Курс'},
    {dataKey: 'CreditScore', title: 'Общие баллы'}
  ]);
  const route = useRoute();
  async function loadData()
  {
    Data.value = [];
    students.value = [];
    let response;
    response = await api.get(`Student?sortKey=${route.query.sortKey}&sortOrder=${route.query.sortType}&page=${route.query.NumberPage}&count=${count}&filter=${route.query.filter}`);
    students.value = response.data.item1 as StudentsType[];
    pageCount.value = response.data.item2 as number;
    students.value.forEach((student) => {
      student.dob = Intl.DateTimeFormat("ru-RU").format(new Date(student.dob));
    })
    debugger;
    for(let i = 0; i < students.value.length; i++) {
      Data.value.push({id: i.toString(), ...students.value[i]});
    }
  }
  let IsWatch = false;
  watch(() => route.query,
  async(newQuery, oldQuery) => {
    debugger;
    if(!IsWatch) return;
    else {
      await loadData();
    }
  },
      {deep: true})
  onMounted(async () => { 
    try {
      debugger;
      IsWatch = false;
      if(route.query.NumberPage == undefined || route.query.NumberPage === null) {
        await router.push({query: {...route.query, NumberPage: 1, filter:JSON.stringify(Filter), sortKey: "null", sortType: "null"}});
      }
      IsWatch = true;
      let response = await api.get(`Student/Page/${count}`);
      pageCount.value = response.data;
      await loadData();
    }
    catch (error) {
      console.error(error);
    }
  })
  const Filter = reactive<Filter>(
      {
        FilterBirthDayStart: undefined,
        FilterBirthDayEnd: undefined,
        FilterSkipHoursStart: undefined,
        FilterSkipHoursEnd: undefined,
        FilterCourse: undefined,
        FilterTotalScore: undefined
      }
  );  
  const optionsCourse = [
    {
      value: "1",
      label: "1 курс"
    },
    {
      value: "2",
      label: "2 курс"
    },
    {
      value: "3",
      label: "3 курс"
    },
    {
      value: "4",
      label: "4 курс"
    },
    {
      value: "5",
      label: "5 курс"
    },
    {
      value: "6",
      label: "6 курс"
    },
    {
      value: undefined,
      label: "Все курсы"
    }
  ]
  const rules : FormRules<typeof Filter> = {
    FilterSkipHoursEnd: [{
      validator: (rule, value : number | undefined, callback) =>
      {
        if(value === undefined || Filter.FilterSkipHoursStart === undefined) {
          return;
        }
        if(value < Filter.FilterSkipHoursStart)
        {
          callback(new Error("Максимальное количество часов пропусков должны быть больше или равно минимальному числу пропусков"))
        }
      }
    }],
    FilterBirthDayEnd: [{
      validator: (rule, value : Date | undefined, callback) =>
      {
        if(value === undefined || Filter.FilterBirthDayStart === undefined) {
          return;
        }
        if(value < Filter.FilterBirthDayStart)
        {
          callback(new Error("Максимальное количество часов пропусков должны быть больше или равно минимальному числу пропусков"))
        }
      }
    }]
  }
  const Reset = () =>
  {
    router.push({query: {...route.query, NumberPage:1, filter:JSON.stringify(Filter)}}); 
  }
  const clearSkipHours = () =>
  {
    Filter.FilterSkipHoursEnd = undefined;
    Filter.FilterSkipHoursStart = undefined;
  }
  const clearData = () =>
  {
    Filter.FilterBirthDayStart = undefined;
    Filter.FilterBirthDayEnd = undefined;
  }
  const SetFilterCourse = (value : any) =>
  {
    Filter.FilterCourse = +value[0];
  }
</script>