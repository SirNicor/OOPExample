<template>
  <el-form inline class = "FilterForm"> 
    <el-form-item>
      <el-date-picker v-model = "Filter.FilterDate" type = "daterange"
                      start-placeholder="Start Date" end-placeholder="End Date"
                      range-separator="To"/>
    </el-form-item>
    <el-form-item label = "Кол-во часов пропусков: ">
      <el-input-number v-model="Filter.FilterSkipHoursStart" :min="1" :max="120" placeholder="От:"/>
      <el-input-number v-model="Filter.FilterSkipHoursEnd" :min="1" :max="120" placeholder="До:" />
    </el-form-item>
    <el-form-item inline>
      <el-cascader :options = "optionsCourse" placeholder="Курс:"  @change = "SetFilterCourse"/>
    </el-form-item> 
    <el-form-item inline>
      <el-input v-model="Filter.FilterTotalScore" placeholder="Средний балл от:"/>
    </el-form-item>
    <el-form-item>
      <el-button type = "primary" @click = "Reset">Фильтровать</el-button>
      <el-button type = "primary" @click = "Clear">Очистить</el-button>
    </el-form-item>
  </el-form>
  <TableComponent :defaultSortKey = "defaultSortKey" :columns="Columns" :data="Data" :apiBase = "Url" :countPage = "pageCount"/>
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
  import {onMounted, reactive, ref, watch} from 'vue'
  import {useRoute} from 'vue-router'
  import TableComponent from '@/components/TableComponent.vue'
  import type {TableColumn, TableData} from '@/types/TableTypes';
  import api from "@/api/Api.ts"
  import {StudentResponse} from "@/api/Student.ts"
  import {type StudentsType, type Filter} from '@/types/studentType.ts'
  import router from '@/router/index.ts'
  import {type FormRules, ElLink } from "element-plus";
  const Url = "/student";
  const students = ref<StudentsType[]>([]);
  const Data = ref<TableData[]>([]);
  const pageCount = ref(0);
  const count = 15;
  const defaultSortKey = "fio";
  const Columns = reactive<TableColumn[]>( [
    {dataKey: 'fio', title: 'ФИО', type: 'string'},
    {dataKey: 'dob', title: 'Дата рождения', type: 'date'},
    {dataKey: 'serial', title: 'Серия', type: 'string'},
    {dataKey: 'number', title: 'Номер', type: 'number'},
    {dataKey: 'address', title: 'Адрес', type: 'string'},
    {dataKey: 'totalScore', title: 'Средний балл', type: 'number'},
    {dataKey: 'skipHours', title: 'Часы пропусков', type: 'number'},
    {dataKey: 'course', title: 'Курс', type: 'number'},
    {dataKey: 'CreditScore', title: 'Общие баллы', type: 'number'},
  ]);
  const Filter = reactive<Filter>(
      {
        FilterDate: undefined,
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
  const route = useRoute();
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
      IsWatch = false;
      if(route.query.NumberPage == undefined || route.query.NumberPage === null) {
        await router.push({query: {...route.query, NumberPage: 1, filter:JSON.stringify(Filter), sortKey: "null", sortType: "null"}});
      }
      IsWatch = true;
      let response = await StudentResponse.getCountPage(count);
      pageCount.value = response.data;
      await loadData();
    }
    catch (error) {
      console.error(error);
    }
  })
  async function loadData()
  {
    Data.value = [];
    students.value = [];
    let response = await StudentResponse.getStudents(route.query.sortKey, route.query.sortType, route.query.NumberPage, route.query.filter, count);
    students.value = response.data.item1 as StudentsType[];
    pageCount.value = response.data.item2 as number;
    for(let i = 0; i < students.value.length; i++) {
      Data.value.push({id: i.toString(), ...students.value[i]});
    }
  }
  const Reset = () =>
  {
    router.push({query: {...route.query, NumberPage:1, filter:JSON.stringify(Filter)}}); 
  }
  const Clear = () =>
  {
    Filter.FilterCourse = undefined;
    Filter.FilterTotalScore = undefined;
    Filter.FilterSkipHoursStart = undefined;
    Filter.FilterSkipHoursEnd = undefined;
    Filter.FilterDate = undefined;
  }
  const SetFilterCourse = (value : any) =>
  {
    Filter.FilterCourse = +value[0];
  }
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
    }]
  }
</script>