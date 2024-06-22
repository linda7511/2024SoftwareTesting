<template>
  <test-panel :context="context" :options="options" :code="code" :versions="versions" :ec-option="ecOption"
    :iteration="iteration">
    <template #header>
      Question 03. 电脑销售问题
    </template>
    <template #sub-title>
      算法思想
    </template>
    <template #detail>
      电脑销售系统，主机（25￥单位价格，每月最多销售的数量为70），显示器（30￥单位价格，每月最多销售数量为80），外设（45￥单位价格，每月最多销售的数量为90）；
      每个销售员每月至少销售一台完整的机器，当系统的主机这个变量接受到-1值的时候，系统自动统计该销售员本月的销售总额。当销售额小于等于1000（包括1000）按照10%提佣金，
      当销售额在1000-1800之间（包括1800）的时候按照15%提佣金，当销售额大于1800时按照20%提佣金。
      <br />① 因为每月至少销售出一台整机，首先判断各配件的销售数量是否大于1；
      <br />② 分别判断各配件是否销售数量超过各自的最大数量；
      <br />③ 若①②条件均成立，可进行销售额计算；
      <br />④ 判断月度销售额在哪个佣金分段，在不同分段获得不同的佣金比例，计算出最终获得的佣金金额。
    </template>
  </test-panel>
</template>

<script setup lang="ts">
import * as echarts from 'echarts/core'
import TestPanel from '../../components/TestPanel.vue'
import type { ECOption } from '@/interface'

// 上下文
const context = 'computerSelling'

// 测试用例集选项
const options = [
  {
    label: '边界值',
    value: 'boundary-value',
    children: [
      {
        label: '基本边界值',
        value: 'basic-boundary',
      },
      {
        label: '健壮边界值',
        value: 'robustness-boundary',
      }
    ]
  }
]

// 实现代码
const code = `function computerSelling(host, monitor, peripheral){
    if (host <= 0 || monitor <= 0 || peripheral <= 0 || host > 70 || monitor > 80 || peripheral > 90) {
        return "数值越界"
    }

    let totalSales = host * 25 + monitor * 30 + peripheral * 45;
    let commission
    if (totalSales <= 1000) {
        commission = totalSales * 0.1;
    } else if (totalSales <= 1800) {
        commission = totalSales * 0.15;
    } else {
        commission = totalSales * 0.2;
    }

    return String(commission)

}`

// 程序版本集
const versions = [
  {
    label: '0.0.0',
    value: '0.0.0'
  },
  {
    label: '0.1.0',
    value: '0.1.0'
  },
]

// ECharts 绘图选项
const ecOption: ECOption = {
  xAxis: {
    type: 'category',
    data: ['v0.0.0', 'v0.1.0']
  },
  yAxis: [
    {
      type: 'value',
      name: '测试用例通过率',
      alignTicks: true,
      position: 'left',
      axisLabel: {
        formatter: '{value} %'
      }
    },
    {
      type: 'value',
      name: '测试用例通过数',
      position: 'right',
    }
  ],
  tooltip: {
    trigger: 'axis'
  },
  toolbox: {
    show: true,
    feature: {
      dataView: { show: true, readOnly: false },
      magicType: { show: true, type: ['line', 'bar'] },
      restore: { show: true },
      saveAsImage: { show: true }
    }
  },
  series: [
    {
      data: [
        {
          value: 94.20,
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#83bff6" },
              { offset: 0.8, color: "#188df0" },
              { offset: 1, color: "#188df0" },
            ]),
          }
        },
        {
          value: 100,
          itemStyle: {
            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: "#83bff6" },
              { offset: 0.8, color: "#188df0" },
              { offset: 1, color: "#188df0" },
            ]),
          }
        }
      ],
      type: 'bar',
      yAxisIndex: 0,
      name: '测试用例通过率',
      tooltip: {
        valueFormatter: (value) => value + ' %'
      }
    },
    {
      data: [
        {
          value: 65,
          itemStyle: {
            color: 'green'
          }
        },
        {
          value: 69,
          itemStyle: {
            color: 'green'
          }
        }
      ],
      type: 'line',
      yAxisIndex: 1,
      markPoint: {
        data: [
          { type: 'max', name: 'Max' }
        ]
      },
      name: '测试用例通过数',
      tooltip: {
        valueFormatter: (value) => value + ' 个'
      }
    }
  ]
}

// 代码版本迭代信息
const iteration = {
  columns: [{
    title: '版本号',
    key: 'version'
  }, {
    title: '测试数据集',
    key: 'dataset'
  }, {
    title: '测试情况',
    key: 'result'
  }, {
    title: '缺陷描述',
    key: 'bug'
  }],
  data: [{
    key: '0',
    version: '0.0.0',
    dataset: '边界值',
    result: '通过65/69',
    bug: '主机数量不能小于1'
  }, {
    key: '1',
    version: '0.1.0',
    dataset: '边界值',
    result: '通过69/69',
    bug: '测试全部通过'
  }]
}
</script>

<style scoped>
</style>